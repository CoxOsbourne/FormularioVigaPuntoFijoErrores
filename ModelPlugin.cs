using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Catalogs;
using Tekla.Structures.Datatype;
using Tekla.Structures.Dialog;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;
using static Tekla.Structures.Filtering.Categories.ReinforcingBarFilterExpressions;
using TSG = Tekla.Structures.Geometry3d;
using TSM = Tekla.Structures.Model;

namespace FormularioVigaPuntoFijoErrores

{
    public class PluginData
    {
        #region Fields
        //
        // Define the fields specified on the Form.
        //
        [StructuresField("Length")]
        public double Length;

        [StructuresField("Profile")]
        public string Profile;

        [StructuresField("Material")]
        public string Material;
        #endregion
    }

    [Plugin("FormularioVigaPuntoFijoErrores")]
    [PluginUserInterface("FormularioVigaPuntoFijoErrores.MainForm")]
    public class FormularioVigaPuntoFijoErrores : PluginBase
    {
        #region Fields
        private Model _Model;
        private PluginData _Data;
        //
        // Define variables for the field values.
        //
        private double _Length;
        private string _Profile;
        private string _Material;
        #endregion

        #region Properties
        private Model Model
        {
            get { return this._Model; }
            set { this._Model = value; }
        }

        private PluginData Data
        {
            get { return this._Data; }
            set { this._Data = value; }
        }
        #endregion

        #region Constructor
        public FormularioVigaPuntoFijoErrores(PluginData data)
        {
            Model = new Model();
            Data = data;
        }
        #endregion

        #region Overrides
        public override List<InputDefinition> DefineInput()
        {
            //
            // This is an example for selecting two points; change this to suit your needs.
            //
            //Devuelvo lista vacía, porque los puntos no se eligen con el ratón...
            return new List<InputDefinition>();
        }

        public override bool Run(List<InputDefinition> Input)
        {
            try
            {
                GetValuesFromDialog();

                //
                // This is an example for selecting two points; change this to suit your needs.
                //
                //ArrayList Points = (ArrayList)Input[0].GetInput();
                //Point StartPoint = Points[0] as Point;
                //Point EndPoint = Points[1] as Point;

                //
                // Write your code here; better yet, create private methods and call them from here.
                //
                if (Model.GetConnectionStatus())
                {
                    Point StartPoint = new Point(0, 0, 0);
                    Point EndPoint = new Point(StartPoint.X + _Length, StartPoint.Y, StartPoint.Z);
                    CreateBeam(StartPoint, EndPoint, _Profile, _Material);
                    Model.CommitChanges();
                }
            }
            catch (Exception Exc)
            {
                MessageBox.Show(Exc.ToString());
            }

            return true;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Gets the values from the dialog and sets the default values if needed
        /// </summary>
        private void GetValuesFromDialog()
        {
            _Length = Data.Length;
            _Profile = Data.Profile;
            _Material = Data.Material;

            if (IsDefaultValue(_Length) || _Length <= 0)
            {
                _Length = 2800;
            }
            if (IsDefaultValue(_Profile))
            {
                _Profile = "HEA200";
            }
            if (IsDefaultValue(_Material))
            {
                _Material = "S235JR";
            }
        }

        // Write your private methods here.
        private TSM.ModelObject CreateBeam(Point Point1, Point Point2, string profile, string material)
        {

            TSM.Beam MyBeam = new TSM.Beam(Point1, Point2);

            MyBeam.Profile.ProfileString = profile;
            MyBeam.Material.MaterialString = material;
            MyBeam.Finish = "PAINT";
            MyBeam.StartPoint.X = Point1.X;
            MyBeam.StartPoint.Y = Point1.Y;
            MyBeam.StartPoint.Z = Point1.Z;
            MyBeam.EndPoint.X = Point2.X;
            MyBeam.EndPoint.Y = Point2.Y;
            MyBeam.EndPoint.Z = Point2.Z;
            MyBeam.Position.Rotation = TSM.Position.RotationEnum.TOP;
            MyBeam.Position.Plane = TSM.Position.PlaneEnum.MIDDLE;
            MyBeam.Position.Depth = TSM.Position.DepthEnum.FRONT;
            if (!MyBeam.Insert())
            {
                Console.WriteLine("Insertion of beam failed.");
            }
            return MyBeam;
        }
        #endregion
    }
}
