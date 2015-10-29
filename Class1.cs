using System;
using Microsoft.VisualBasic;

namespace QuasiAddIn1
{
	public class Class1 : IDisposable
	{
		
		private string _ModuleDescr = "vb Sample App Tests";
public string ModuleDescr
		{
			get
			{
				return _ModuleDescr;
			}
			set
			{
				_ModuleDescr = value;
			}
		}
		private string _ModuleID = "quasiAddIn1";
public string ModuleID
		{
			get
			{
				return _ModuleID;
			}
			set
			{
				_ModuleID = value;
			}
		}
		
public bool QuasiAddIn
		{
			//property signaling to Quasi97 that this is valid Quasi97 add-in, should return true
			get
			{
				return true;
			}
		}
		
		public void Initialize2(object QSTPtr)
		{
			//entry point, the function Quasi97 calls after instantiating this class
			try
			{
				modMain.QST = (Quasi97.Application)QSTPtr;
				modMain.QST.QstStatus.Message = "Hello QST!";
				
				modMain.QST.QuasiParameters.RegisterTestClassNET(clsTest1.ThisTestID, "QuasiAddIn1", "QuasiAddIn1.clsTest1", "", "Quasi97.ucGenericNoGraph");
				modMain.QST.QuasiParameters.RegisterTestClassNET(clsTest2.ThisTestID, "QuasiAddIn1", "QuasiAddIn1.clsTest2", "", "Quasi97.ucGenericNoGraph");
				
			}
			catch (Exception ex)
			{
				Interaction.MsgBox("Error occured: " + ex.Message, (Microsoft.VisualBasic.MsgBoxStyle) 0, null);
			}
		}
		
#region IDisposable Support
		private bool disposedValue; // To detect redundant calls
		
		// IDisposable
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				if (disposing)
				{
					if (!(modMain.QST == null))
					{
						modMain.QST.QuasiParameters.UnregisterTestClass2(clsTest1.ThisTestID);
						modMain.QST.QuasiParameters.UnregisterTestClass2(clsTest2.ThisTestID);
						modMain.QST = null;
					}
				}
			}
			this.disposedValue = true;
		}
		
		~Class1()
		{
			// Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
			Dispose(false);
			//base.Finalize();
		}
		
		// This code added by Visual Basic to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
			Dispose(true);
			GC.SuppressFinalize(this);
		}
#endregion
		
		public Class1()
		{
			
		}
	}
	
}
