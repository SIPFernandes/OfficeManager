using Microsoft.AspNetCore.Components;

namespace OfficeManagerApp.Shared
{
    public partial class Stepper : ComponentBase
    {
        [Parameter]
        public IList<string> StepList { get; set; }

        [Parameter]
        public int StepNumber { get; set; }
    }
}
