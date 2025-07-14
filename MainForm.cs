using System;

namespace FormularioVigaPuntoFijoErrores
{
    public partial class MainForm : Tekla.Structures.Dialog.PluginFormBase
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void OkApplyModifyGetOnOffCancel_OkClicked(object sender, EventArgs e)
        {
            this.Apply();
            this.Close();
        }

        private void OkApplyModifyGetOnOffCancel_ApplyClicked(object sender, EventArgs e)
        {
            this.Apply();
        }

        private void OkApplyModifyGetOnOffCancel_ModifyClicked(object sender, EventArgs e)
        {
            this.Modify();
        }

        private void OkApplyModifyGetOnOffCancel_GetClicked(object sender, EventArgs e)
        {
            this.Get();
        }

        private void OkApplyModifyGetOnOffCancel_OnOffClicked(object sender, EventArgs e)
        {
            this.ToggleSelection();
        }

        private void OkApplyModifyGetOnOffCancel_CancelClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void profileCatalogSelectClicked(object sender, EventArgs e)
        {
            profileCatalog.SelectedProfile=textBoxProfile.Text;
        }

        private void profileCatalogSelectionDone(object sender, EventArgs e)
        {
            SetAttributeValue(textBoxProfile,profileCatalog.SelectedProfile);
        }

        private void materialCatalogSelectClicked(object sender, EventArgs e)
        {
            materialCatalog.SelectedMaterial=textBoxMaterial.Text;
        }

        private void materialCatalogSelectionDone(object sender, EventArgs e)
        {
            SetAttributeValue(textBoxMaterial,materialCatalog.SelectedMaterial);
        }

        private void buttonCreateBeam_Click(object sender, EventArgs e)
        {

        }
    }
}