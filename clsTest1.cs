using System;
using System.Windows.Forms;

namespace QuasiAddIn1
{
	public class clsTest1 : Quasi97.clsQSTTestNET
	{
		
		public const string ThisTestID = "Head Voltage Test";
		private NativeRes objNativeRes = new NativeRes();
		private short mvarAverages = (short) 1;
		
		private class NativeRes
		{
			public string bv = "Bias Voltage";
			public string res = "Resistance (Ohm)";
		}
		
public short Average
		{
			get
			{
				return mvarAverages;
			}
			set
			{
				if (value < 1 | value > 100)
				{
					throw (new System.ArgumentOutOfRangeException("Average", value, "Out of range 1..100 [" + System.Convert.ToString(value) + "]"));
				}
				else
				{
					mvarAverages = value;
				}
			}
		}
		
		public override void RunTest()
		{
			Quasi97.ResultNet Rslt = default(Quasi97.ResultNet);
			double MeasuredVal = 0;
			float ib = 0;
			
			try
			{
				
				string temp_TestID = TestID;
				modMain.QST.Normalization.InitTest(ref temp_TestID, ref Setup);
				string temp_TestID2 = TestID;
				string temp_paramName = "";
				object temp_OriginalValue = 0;
				modMain.QST.Normalization.GetAdaptParam(ref temp_TestID2, ref Setup, ref temp_paramName, ref temp_OriginalValue);
				//measuring
				MeasuredVal = modMain.QST.ChannelManager.DCChannel.MeasureR(modMain.QST.QSTHardware.MRChannel, (short) 10,0);
				ib = modMain.QST.QSTHardware.GetReadBias(modMain.QST.QSTHardware.MRChannel);
				
				//report results
				Rslt = new Quasi97.ResultNet();
				Rslt.AddParameters(this.colParameters);
				colResults.Insert(0, Rslt);
				short temp_cycle = 1;
				Rslt.AddResult("Resistance (Ohm)", MeasuredVal.ToString("F2"), ref temp_cycle, false);
				Rslt.AddResult(objNativeRes.bv, (ib * MeasuredVal).ToString("F2"), ref temp_cycle, false);
                int strtcol = 1; 
                Rslt.CalcStats("RESULT", ref strtcol);
				
				string temp_TestID3 = TestID;
				string temp_paramName2 = "";
				modMain.QST.Normalization.AddResultforRecordNET(ref temp_TestID3, ref Setup, ref temp_paramName2, ref Rslt); //get all parameters, because we are not sweeping anything
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
				MessageBox.Show("clsTest1:RunTest " + ex.Message);
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
		
		public clsTest1()
		{
			base.RegisterResults2(objNativeRes);
			base.colParameters.Add(new Quasi97.clsTestParam(this,"Average", "Average", Average.GetType(), true, null));
			
		}
	}
	
}
