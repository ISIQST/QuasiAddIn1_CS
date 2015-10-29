using System;
using System.Linq;
using System.Windows.Forms;

namespace QuasiAddIn1
{
	public class clsTest2 : Quasi97.clsQSTTestNET
	{
		
		public const string ThisTestID = "NETSample 5";
		private NativeRes objNativeRes = new NativeRes();
		
		private class NativeRes
		{
			public string bv = "Head Voltage (mV)";
			public string res = "Resistance (Ohm)";
		}
		
		public override void RunTest()
		{
			Quasi97.ResultNet Rslt = default(Quasi97.ResultNet);
            double MeasuredVal = 0;
			float ib = 0;
			Quasi97.TestShell ts = default(Quasi97.TestShell);
			
			try
			{
				//measuring
				MeasuredVal = modMain.QST.ChannelManager.DCChannel.MeasureR(modMain.QST.QSTHardware.MRChannel, (short) 10);
				ib = modMain.QST.QSTHardware.GetReadBias(modMain.QST.QSTHardware.MRChannel);
				
				//report results
				Rslt = new Quasi97.ResultNet();
				colResults.Insert(0, Rslt);
				
				//example of scanning test collection
				var tKey = "";
				foreach (var t in modMain.QST.QuasiParameters.TestListLegacy)
				{
					if (t.Value.TestID == "Transverse")
					{
						tKey = t.Key;
					}
				}
				if (tKey != "")
				{
					ts = modMain.QST.QuasiParameters.TestListLegacy[tKey];
				}
				
				//getting result from another test
				string temp_TestID = "Transverse";
				short temp_Setup = 1;
				ts = modMain.QST.QuasiParameters.TestObj(ref temp_TestID, ref temp_Setup);
                Quasi97.clsQSTTestNET tnet = (Quasi97.clsQSTTestNET) ts.TestPtr;
				if (tnet.colResults.Count == 0)
				{
					ts.RunTest();
				}
				
				Quasi97.clsQSTTestNET tobj = (Quasi97.clsQSTTestNET)ts.TestPtr;
				MeasuredVal = System.Convert.ToSingle(tobj.colResults[0].GetResult("Resistance (Ohm)"));
				ib = System.Convert.ToSingle(tobj.colResults[0].GetResult("Bias Current (mA)"));
				
				
				//adding results
				short temp_cycle = 1;
				Rslt.AddResult(objNativeRes.res, MeasuredVal.ToString("F2"), ref temp_cycle, false);
				Rslt.AddResult(objNativeRes.bv, (ib * MeasuredVal).ToString("F2"), ref temp_cycle, false);
                int startcol=1;
                Rslt.CalcStats("RESULT", ref startcol);
				
				//grade results
				Quasi97.clsQSTTestNET temp_test = this;
				modMain.QST.GradingParameters.GradeTestNet(ref temp_test, (short) 0);
				
				//add information about run
				Quasi97.clsQSTTestNET temp_test2 = this;
				Rslt.AddInfo(ref temp_test2, 0, ref modMain.QST.QuasiParameters.CurInfo);
				
				//notify the form that new results are available
				base.RaiseNewInfoAvailable();
				base.RaiseNewResultsAvailable(new int[] {0});
				
			}
			catch (Exception ex)
			{
				MessageBox.Show("clsTest2:RunTest " + ex.Message);
			}
		}
		
public override string TestID
		{
			get
			{
				return ThisTestID;
			}
		}
		
		public override System.Collections.Generic.List<short> CheckRecords(string NewDBase)
		{
			return new System.Collections.Generic.List<short>();
		}
		
		public override void ClearResults(bool doRefreshPlot = false)
		{
			colResults.Clear();
			base.RaiseResultsCleared(doRefreshPlot);
		}
		
		public override void RemoveRecord()
		{
			
		}
		
		public override void RestoreParameters()
		{
			
		}
		
		public override void SetDBase(ref string NewDBase, ref object voidParam)
		{
			
		}
		
		public override void StoreParameters()
		{
			
		}
		
public override short ContainsGraph
		{
			get
			{
				return 0;
			}
		}
		
public override bool ContainsResultPerCycle
		{
			get
			{
				return false;
			}
		}
		
public override bool DualChannelCapable
		{
			get
			{
				return false;
			}
		}
		
public override UInt32 FeatureVector
		{
			get
			{
				return 0;
			}
		}
		
		public clsTest2()
		{
			base.RegisterResults2(objNativeRes);
		}
	}
	
}
