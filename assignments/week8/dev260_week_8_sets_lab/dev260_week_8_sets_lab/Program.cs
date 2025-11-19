using System;

namespace dev260_week_8_sets_lab
{
    class Program
    {
		static void Main(string[] args)
		{
			var lab = new SetOperationsLab();
			LabSupport.LoadDemoData(lab);
			LabSupport.RunInteractiveMenu(lab);
		}
    }
}