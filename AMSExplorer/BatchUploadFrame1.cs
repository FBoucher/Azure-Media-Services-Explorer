﻿//----------------------------------------------------------------------------------------------
//    Copyright 2015 Microsoft Corporation
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//---------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace AMSExplorer
{
    public partial class BatchUploadFrame1 : Form
    {
        public string BatchFolder
        {
            get
            {
                return textBoxFolder.Text;
            }
            set
            {
                textBoxFolder.Text = value;
            }
        }

        public bool BatchProcessSubFolders
        {
            get
            {
                return checkBoxSubFolder.Checked;
            }
            set
            {
                checkBoxSubFolder.Checked = value;
            }
        }
        public bool BatchProcessFiles
        {
            get
            {
                return checkBoxProcessFiles.Checked;
            }
            set
            {
                checkBoxProcessFiles.Checked = value;
            }
        }

        public AssetCreationOptions EncryptionOption
        {
            get
            {
                return  (AssetCreationOptions)Enum.Parse(typeof(AssetCreationOptions), (comboBoxEncryption.SelectedItem as Item).Value);
            }
        }



        public BatchUploadFrame1()
        {
            InitializeComponent();
        }

        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog openFolderDialog = new CommonOpenFileDialog() { IsFolderPicker = true };
            if (openFolderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                textBoxFolder.Text = openFolderDialog.FileName;// FolderDialog.SelectedPath;
            }
        }

        private void BathUploadFrame1_Load(object sender, EventArgs e)
        {
            checkBoxOneUpDownload.Checked = Properties.Settings.Default.useTransferQueue;
            //checkBoxUseStorageEncryption.Checked = Properties.Settings.Default.useStorageEncryption;

            comboBoxEncryption.Items.Add(new Item("None (no encryption before upload)", AssetCreationOptions.None.ToString()));
            comboBoxEncryption.Items.Add(new Item("Storage encryption (content will be encrypted locally to AES 256 before upload)", AssetCreationOptions.StorageEncrypted.ToString()));
            comboBoxEncryption.Items.Add(new Item("Common encryption (content is already encrypted with PlayReady)", AssetCreationOptions.CommonEncryptionProtected.ToString()));
            if (Properties.Settings.Default.useStorageEncryption)
            {
                comboBoxEncryption.SelectedIndex = 1;
            }
            else
            {
                comboBoxEncryption.SelectedIndex = 0;
            }

        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBoxFolder.Text))
            {
                MessageBox.Show("Folder does not exist", "Folder", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
