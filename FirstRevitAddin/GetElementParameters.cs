using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace FirstRevitAddin
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class GetElementParameters : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get application and documnet objects
            var uiapp = commandData.Application;
            var doc = uiapp.ActiveUIDocument.Document;

            //Define a reference Object to accept the pick result
            Reference pickedRef = null;

            //Pick a group
            var sel = uiapp.ActiveUIDocument.Selection;
            pickedRef = sel.PickObject(ObjectType.Element, "Please select an element");

            var elem = doc.GetElement(pickedRef);

            var objectParameters = "";

            foreach (var parameter in elem.Parameters)

            {
                var revitParameter = (Parameter) parameter;
                objectParameters +=
                    $"{revitParameter.Definition.Name} : {revitParameter.AsValueString()} \n";
            }

            TaskDialog.Show("Element Parameters and Values", objectParameters);

            return Result.Succeeded;
        }
    }
}