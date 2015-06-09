// ==============================================================================
//
//  File:							Zoll_2015.CS
//
//
//  Purpose:						Tests a series of batteries per the customers
//									specifications.
//
//	Requirements:					MiniLab 1 & 2 must be connected for the program
//									to run. They must have their serial numbers set
//									to 1 & 2. The relay card must be connected to serial
//									number 1. The .net framework must be installed.
//									The file MccDac.dll must be in the same directory
//									as the execuitable. Default directory structure
//									is C:\AVED-Zoll.
//
//	Author:							Richard W. Bryant
//
//	Copyright:						2005 - 2015 Elizabeth Sue Productions, Ltd.
//
// ==============================================================================

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Runtime.InteropServices;

namespace Zoll_2015
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Zoll_2015 : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		
		private System.Windows.Forms.Label lblBattery1;
		private System.Windows.Forms.Label lblBattery2;
		private System.Windows.Forms.Label lblBattery3;
		private System.Windows.Forms.Label lblBattery4;
		private System.Windows.Forms.Label lblBattery5;
		private System.Windows.Forms.Label lblBattery6;
		private System.Windows.Forms.Label lblBattery7;
		private System.Windows.Forms.Label lblBattery8;

		private System.Windows.Forms.Label lblSerialNums;
		private System.Windows.Forms.Label lblSerNum1;
		private System.Windows.Forms.Label lblSerNum2;
		private System.Windows.Forms.Label lblSerNum3;
		private System.Windows.Forms.Label lblSerNum4;
		private System.Windows.Forms.Label lblSerNum5;
		private System.Windows.Forms.Label lblSerNum6;
		private System.Windows.Forms.Label lblSerNum7;
		private System.Windows.Forms.Label lblSerNum8;

		private System.Windows.Forms.Label lblOcVoltage;
		private System.Windows.Forms.Label lblOcV1;
		private System.Windows.Forms.Label lblOcV2;
		private System.Windows.Forms.Label lblOcV3;
		private System.Windows.Forms.Label lblOcV4;
		private System.Windows.Forms.Label lblOcV5;
		private System.Windows.Forms.Label lblOcV6;
		private System.Windows.Forms.Label lblOcV7;
		private System.Windows.Forms.Label lblOcV8;

		private System.Windows.Forms.Label lblLoadV;
		private System.Windows.Forms.Label lblLoadV1;
		private System.Windows.Forms.Label lblLoadV2;
		private System.Windows.Forms.Label lblLoadV3;
		private System.Windows.Forms.Label lblLoadV4;
		private System.Windows.Forms.Label lblLoadV5;
		private System.Windows.Forms.Label lblLoadV6;
		private System.Windows.Forms.Label lblLoadV7;
		private System.Windows.Forms.Label lblLoadV8;

		private System.Windows.Forms.Label lblNoLoadV;
		private System.Windows.Forms.Label lblNoLoadV1;
		private System.Windows.Forms.Label lblNoLoadV2;
		private System.Windows.Forms.Label lblNoLoadV3;
		private System.Windows.Forms.Label lblNoLoadV4;
		private System.Windows.Forms.Label lblNoLoadV5;
		private System.Windows.Forms.Label lblNoLoadV6;
		private System.Windows.Forms.Label lblNoLoadV7;
		private System.Windows.Forms.Label lblNoLoadV8;

		private System.Windows.Forms.CheckBox chkIDRes;
		private System.Windows.Forms.Label lblID;
		private System.Windows.Forms.Label lblID1;
		private System.Windows.Forms.Label lblID2;
		private System.Windows.Forms.Label lblID3;
		private System.Windows.Forms.Label lblID4;
		private System.Windows.Forms.Label lblID5;
		private System.Windows.Forms.Label lblID6;
		private System.Windows.Forms.Label lblID7;
		private System.Windows.Forms.Label lblID8;

		private System.Windows.Forms.Label lblResult;
		private System.Windows.Forms.Label lblResult1;
		private System.Windows.Forms.Label lblResult2;
		private System.Windows.Forms.Label lblResult3;
		private System.Windows.Forms.Label lblResult4;
		private System.Windows.Forms.Label lblResult5;
		private System.Windows.Forms.Label lblResult6;
		private System.Windows.Forms.Label lblResult7;
		private System.Windows.Forms.Label lblResult8;

		private System.Windows.Forms.Label lblTestTitle;
		private System.Windows.Forms.Label lblStartTime;
		private System.Windows.Forms.TextBox txtLotNum;
		private System.Windows.Forms.Label lblLotNum;
		private System.Windows.Forms.Label lblFileName;
		private System.Windows.Forms.Button btnSelectFile;
		private System.Windows.Forms.TextBox txtPoNumbr;
		private System.Windows.Forms.Label lblPoNumbr;
		private System.Windows.Forms.TextBox txtCellCode;
		private System.Windows.Forms.Label lblCellCode;

		private System.Windows.Forms.Label lblPackSerNums;
		private System.Windows.Forms.TextBox txtPackSerNum1;
		private System.Windows.Forms.TextBox txtPackSerNum2;
		private System.Windows.Forms.TextBox txtPackSerNum3;
		private System.Windows.Forms.TextBox txtPackSerNum4;
		private System.Windows.Forms.TextBox txtPackSerNum5;
		private System.Windows.Forms.TextBox txtPackSerNum6;
		private System.Windows.Forms.TextBox txtPackSerNum7;
		private System.Windows.Forms.TextBox txtPackSerNum8;
		private System.Windows.Forms.Label lblTestComplete;

		private System.Windows.Forms.Label[] lblSerialNum;
		private System.Windows.Forms.Label[] lblPreTestVtg;
		private System.Windows.Forms.Label[] lblLoadVtg;
		private System.Windows.Forms.Label[] lblNoLoadVtg;
		private System.Windows.Forms.Label[] lblResults;
		private System.Windows.Forms.Label[] lblShowData;
		private System.Windows.Forms.Label[] lblIDRes;
		private System.Windows.Forms.TextBox[] txtPackSerNum;
		
		private System.Windows.Forms.Label lblEnterSerNum;
		private System.Windows.Forms.TextBox txtStartSerNum;
		private System.Windows.Forms.Button btnAccept;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label lblElapsed;
		private System.Windows.Forms.Label lblElapsedTime;

		private System.Windows.Forms.Label lblStartDateTime;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btnQuit;
		private System.Windows.Forms.Label lblStopDateTime;

		// The following items are for the A/D and DIO hardware

		const int NumPoints = 100;    //  Number of data points to collect
		const int FirstPoint = 0;     //  set first element in buffer to transfer to array

		private MccDaq.MccBoard DaqBoard1;
		private MccDaq.MccBoard DaqBoard2;

		//  dimension an array to hold the input values
		//  by Windows through MccDaq.MccService.WinBufAlloc()
		private int MemHandle1 = 0;		//  define a variable to contain the handle for memory allocated 
		private int MemHandle2 = 0;		//  define a variable to contain the handle for memory allocated 

		// Define some values for the relay board
		const MccDaq.DigitalPortType PortNum = MccDaq.DigitalPortType.FirstPortA;	//  set port A to use
		const MccDaq.DigitalPortType PortNumA = MccDaq.DigitalPortType.FirstPortA;	//  set port A to use
		const MccDaq.DigitalPortType PortNumB = MccDaq.DigitalPortType.FirstPortB;	//  set port B to use
		const MccDaq.DigitalPortType PortNumCL = MccDaq.DigitalPortType.FirstPortCL;	//  set port B to use
		const MccDaq.DigitalPortType PortNumCH = MccDaq.DigitalPortType.FirstPortCH;	//  set port B to use
		const MccDaq.DigitalPortDirection Direction = MccDaq.DigitalPortDirection.DigitalOut; //  program digital port for output

		ushort TestMask = 0;		// Written to PortNumA to set/clear load relays
		ushort PassMask = 0;		// Written to PortNumB to set pass leds
		ushort FailMask = 0;
		ushort FailMaskHigh = 0;	// Written to PortNumCH to set fail leds
		ushort FailMaskLow = 0;		// Written to PortNumCl to set fail leds

		private int MaxChan;

		private ushort[] ADData = new ushort[NumPoints];
		private int counter;				// Number of seconds test runs
		private int elapsedTimeCtr;		// Time into test
		private int NextStartSerialNum;	// Next starting serial number
		private float Threshold;		// Threshold voltage to decide if battery attached
		private float[] PreTestVtg = new float[8];
		private float[,] IDTestVtgOc1 = new float[8,4];
		private float[,] IDTestVtgID = new float[8,4];
		private float[,] IDTestVtgOc2 = new float[8,4];
		private float[] LoadVtg = new float[8];
		private float[] NoLoadVtg = new float[8];
		private float[] IDRes = new float[8];
		private float[] fudge = new float[8];
		private float[] SaveEngUnits;

		private string[] strMechanicalFit = new string[8];
        private string[] strPackSerNum = new string[8];     //Holds complete long pack number - rwb 5/27/2015

		private bool[] TestChannel = new bool[8];
		private bool GotASerialNum = false;

		private int Board1SerialNum;
		private int Board2SerialNum;
		private int BoardNum;
		private int InfoType = 2;		// Board Information
		private int ConfigType = 214;	// Board serial number

		private int StartSerialNum = 1;
		private string StartSerNumTxt;
		private string WeekYearTxt;
		private System.Windows.Forms.CheckBox chkIDResOnly;
        private TextBox txtTestTitle;
        private Label lblTestStatus;
		private string SerNumTxt;

		/// <summary>
		/// Required designer variable to get board serial number.
		/// </summary>
		[DllImport("cbw32.dll")]
		internal static extern int cbGetConfig (int InfoType, int BoardNum, int DevNum,int ConfigItem, out int ConfigVal);

		public Zoll_2015()
		{
			MccDaq.ErrorInfo ULStat1;
			MccDaq.ErrorInfo ULStat2;

			//
			// Required for Windows Form Designer support
			//

			InitializeComponent();

			//  Initiate error handling
			//   activating error handling will trap errors like
			//   bad channel numbers and non-configured conditions.
			//   Parameters:
			//     MccDaq.ErrorReporting.PrintAll :all warnings and errors encountered will be printed
			//     MccDaq.ErrorHandling.StopAll   :if an error is encountered, the program will stop

//	Comment the following 2 lines out if no hardware is attached		
			ULStat1 = MccDaq.MccService.ErrHandling(MccDaq.ErrorReporting.PrintAll, MccDaq.ErrorHandling.StopAll);
			ULStat2 = MccDaq.MccService.ErrHandling(MccDaq.ErrorReporting.PrintAll, MccDaq.ErrorHandling.StopAll);
		
			// Create a new MccBoard object for Board 1 & 2
			DaqBoard1 = new MccDaq.MccBoard(1);
			DaqBoard2 = new MccDaq.MccBoard(2);

			// Allocate memory buffer to hold data..
			MemHandle1 = MccDaq.MccService.WinBufAlloc(NumPoints); //  set aside memory to hold data
			MemHandle2 = MccDaq.MccService.WinBufAlloc(NumPoints); //  set aside memory to hold data
		
			//  This gives us access to labels via an indexed array

			lblSerialNum = (new System.Windows.Forms.Label[] {this.lblSerNum1, this.lblSerNum2, this.lblSerNum3, this.lblSerNum4,
																 this.lblSerNum5, this.lblSerNum6, this.lblSerNum7, this.lblSerNum8});

			txtPackSerNum = (new System.Windows.Forms.TextBox[] {this.txtPackSerNum1, this.txtPackSerNum2, this.txtPackSerNum3, this.txtPackSerNum4,
																 this.txtPackSerNum5, this.txtPackSerNum6, this.txtPackSerNum7, this.txtPackSerNum8});

			lblPreTestVtg = (new System.Windows.Forms.Label[] {this.lblOcV1, this.lblOcV2, this.lblOcV3, this.lblOcV4,
																  this.lblOcV5, this.lblOcV6, this.lblOcV7, this.lblOcV8});

			lblLoadVtg = (new System.Windows.Forms.Label[] {this.lblLoadV1, this.lblLoadV2, this.lblLoadV3, this.lblLoadV4,
															   this.lblLoadV5, this.lblLoadV6, this.lblLoadV7, this.lblLoadV8});

			lblNoLoadVtg = (new System.Windows.Forms.Label[] {this.lblNoLoadV1, this.lblNoLoadV2, this.lblNoLoadV3, this.lblNoLoadV4,
																 this.lblNoLoadV5, this.lblNoLoadV6, this.lblNoLoadV7, this.lblNoLoadV8});

			lblIDRes = (new System.Windows.Forms.Label[] {this.lblID1, this.lblID2, this.lblID3, this.lblID4,
															 this.lblID5, this.lblID6, this.lblID7, this.lblID8});

			lblResults = (new System.Windows.Forms.Label[] {this.lblResult1, this.lblResult2, this.lblResult3, this.lblResult4,
															   this.lblResult5, this.lblResult6, this.lblResult7, this.lblResult8});

			Threshold = 11.0F;			// Set threshold voltage to 11 volts.

			BoardNum = 1;		// Get board 1 serial #
			cbGetConfig(InfoType,BoardNum, 0, ConfigType, out Board1SerialNum);
	
			BoardNum = 2;		// Get board 2 serial #
			cbGetConfig(InfoType,BoardNum, 0, ConfigType, out Board2SerialNum);

			// The relay board is connected to the miniLAB with serial Number 1
			if(Board1SerialNum == 1)
			{
				ULStat1 = DaqBoard1.DConfigPort(PortNumA, Direction);
				ULStat1 = DaqBoard1.DConfigPort(PortNumB, Direction);
				ULStat1 = DaqBoard1.DConfigPort(PortNumCL, Direction);
				ULStat1 = DaqBoard1.DConfigPort(PortNumCH, Direction);
			}

			if(Board2SerialNum == 1)
			{
				ULStat2 = DaqBoard2.DConfigPort(PortNumA, Direction);
				ULStat2 = DaqBoard2.DConfigPort(PortNumB, Direction);
				ULStat2 = DaqBoard2.DConfigPort(PortNumCL, Direction);
				ULStat2 = DaqBoard2.DConfigPort(PortNumCH, Direction);
			}

            lblTestStatus.Text = "";    // Initialize the test status - rwb 5/27/2015
            txtStartSerNum.Text = "";	// Initialize the starting serial number
			txtLotNum.Text = "";	// Initialize the Lot number
			txtPoNumbr.Text = "";	// Initialize the Purchase order number
			txtCellCode.Text = "";	// Initialize the Cell code
			lblFileName.Text = "";
			ClearLabels(true);		// Clear the data label text fields
			ClearAllRelays();		// Clear all the relays
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Zoll_2015));
            this.lblBattery1 = new System.Windows.Forms.Label();
            this.lblSerialNums = new System.Windows.Forms.Label();
            this.lblBattery2 = new System.Windows.Forms.Label();
            this.lblBattery3 = new System.Windows.Forms.Label();
            this.lblBattery4 = new System.Windows.Forms.Label();
            this.lblBattery5 = new System.Windows.Forms.Label();
            this.lblBattery6 = new System.Windows.Forms.Label();
            this.lblBattery7 = new System.Windows.Forms.Label();
            this.lblBattery8 = new System.Windows.Forms.Label();
            this.lblSerNum1 = new System.Windows.Forms.Label();
            this.lblSerNum2 = new System.Windows.Forms.Label();
            this.lblSerNum3 = new System.Windows.Forms.Label();
            this.lblSerNum4 = new System.Windows.Forms.Label();
            this.lblSerNum5 = new System.Windows.Forms.Label();
            this.lblSerNum6 = new System.Windows.Forms.Label();
            this.lblSerNum7 = new System.Windows.Forms.Label();
            this.lblSerNum8 = new System.Windows.Forms.Label();
            this.lblOcVoltage = new System.Windows.Forms.Label();
            this.lblOcV1 = new System.Windows.Forms.Label();
            this.lblOcV2 = new System.Windows.Forms.Label();
            this.lblOcV3 = new System.Windows.Forms.Label();
            this.lblOcV4 = new System.Windows.Forms.Label();
            this.lblOcV5 = new System.Windows.Forms.Label();
            this.lblOcV6 = new System.Windows.Forms.Label();
            this.lblOcV7 = new System.Windows.Forms.Label();
            this.lblOcV8 = new System.Windows.Forms.Label();
            this.lblLoadV = new System.Windows.Forms.Label();
            this.lblLoadV1 = new System.Windows.Forms.Label();
            this.lblLoadV2 = new System.Windows.Forms.Label();
            this.lblLoadV3 = new System.Windows.Forms.Label();
            this.lblLoadV4 = new System.Windows.Forms.Label();
            this.lblLoadV5 = new System.Windows.Forms.Label();
            this.lblLoadV6 = new System.Windows.Forms.Label();
            this.lblLoadV7 = new System.Windows.Forms.Label();
            this.lblLoadV8 = new System.Windows.Forms.Label();
            this.lblNoLoadV = new System.Windows.Forms.Label();
            this.lblNoLoadV1 = new System.Windows.Forms.Label();
            this.lblNoLoadV2 = new System.Windows.Forms.Label();
            this.lblNoLoadV3 = new System.Windows.Forms.Label();
            this.lblNoLoadV4 = new System.Windows.Forms.Label();
            this.lblNoLoadV5 = new System.Windows.Forms.Label();
            this.lblNoLoadV6 = new System.Windows.Forms.Label();
            this.lblNoLoadV7 = new System.Windows.Forms.Label();
            this.lblNoLoadV8 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblID1 = new System.Windows.Forms.Label();
            this.lblID2 = new System.Windows.Forms.Label();
            this.lblID3 = new System.Windows.Forms.Label();
            this.lblID4 = new System.Windows.Forms.Label();
            this.lblID5 = new System.Windows.Forms.Label();
            this.lblID6 = new System.Windows.Forms.Label();
            this.lblID7 = new System.Windows.Forms.Label();
            this.lblID8 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblResult1 = new System.Windows.Forms.Label();
            this.lblResult2 = new System.Windows.Forms.Label();
            this.lblResult3 = new System.Windows.Forms.Label();
            this.lblResult4 = new System.Windows.Forms.Label();
            this.lblResult5 = new System.Windows.Forms.Label();
            this.lblResult6 = new System.Windows.Forms.Label();
            this.lblResult7 = new System.Windows.Forms.Label();
            this.lblResult8 = new System.Windows.Forms.Label();
            this.lblEnterSerNum = new System.Windows.Forms.Label();
            this.txtStartSerNum = new System.Windows.Forms.TextBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblElapsed = new System.Windows.Forms.Label();
            this.lblElapsedTime = new System.Windows.Forms.Label();
            this.lblStartDateTime = new System.Windows.Forms.Label();
            this.lblStopDateTime = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTestTitle = new System.Windows.Forms.Label();
            this.btnQuit = new System.Windows.Forms.Button();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblTestComplete = new System.Windows.Forms.Label();
            this.txtLotNum = new System.Windows.Forms.TextBox();
            this.lblLotNum = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtPoNumbr = new System.Windows.Forms.TextBox();
            this.lblPoNumbr = new System.Windows.Forms.Label();
            this.txtCellCode = new System.Windows.Forms.TextBox();
            this.lblCellCode = new System.Windows.Forms.Label();
            this.lblPackSerNums = new System.Windows.Forms.Label();
            this.txtPackSerNum1 = new System.Windows.Forms.TextBox();
            this.txtPackSerNum2 = new System.Windows.Forms.TextBox();
            this.txtPackSerNum3 = new System.Windows.Forms.TextBox();
            this.txtPackSerNum4 = new System.Windows.Forms.TextBox();
            this.txtPackSerNum5 = new System.Windows.Forms.TextBox();
            this.txtPackSerNum6 = new System.Windows.Forms.TextBox();
            this.txtPackSerNum7 = new System.Windows.Forms.TextBox();
            this.txtPackSerNum8 = new System.Windows.Forms.TextBox();
            this.chkIDRes = new System.Windows.Forms.CheckBox();
            this.chkIDResOnly = new System.Windows.Forms.CheckBox();
            this.txtTestTitle = new System.Windows.Forms.TextBox();
            this.lblTestStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBattery1
            // 
            this.lblBattery1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBattery1.Location = new System.Drawing.Point(24, 264);
            this.lblBattery1.Name = "lblBattery1";
            this.lblBattery1.Size = new System.Drawing.Size(80, 16);
            this.lblBattery1.TabIndex = 0;
            this.lblBattery1.Text = "Battery 1  -";
            this.lblBattery1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSerialNums
            // 
            this.lblSerialNums.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialNums.Location = new System.Drawing.Point(111, 216);
            this.lblSerialNums.Name = "lblSerialNums";
            this.lblSerialNums.Size = new System.Drawing.Size(112, 32);
            this.lblSerialNums.TabIndex = 19;
            this.lblSerialNums.Text = "Pack Serial Number";
            this.lblSerialNums.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBattery2
            // 
            this.lblBattery2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBattery2.Location = new System.Drawing.Point(24, 296);
            this.lblBattery2.Name = "lblBattery2";
            this.lblBattery2.Size = new System.Drawing.Size(80, 16);
            this.lblBattery2.TabIndex = 29;
            this.lblBattery2.Text = "Battery 2  -";
            this.lblBattery2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBattery3
            // 
            this.lblBattery3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBattery3.Location = new System.Drawing.Point(24, 328);
            this.lblBattery3.Name = "lblBattery3";
            this.lblBattery3.Size = new System.Drawing.Size(80, 16);
            this.lblBattery3.TabIndex = 30;
            this.lblBattery3.Text = "Battery 3  -";
            this.lblBattery3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBattery4
            // 
            this.lblBattery4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBattery4.Location = new System.Drawing.Point(24, 360);
            this.lblBattery4.Name = "lblBattery4";
            this.lblBattery4.Size = new System.Drawing.Size(80, 16);
            this.lblBattery4.TabIndex = 31;
            this.lblBattery4.Text = "Battery 4  -";
            this.lblBattery4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBattery5
            // 
            this.lblBattery5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBattery5.Location = new System.Drawing.Point(24, 392);
            this.lblBattery5.Name = "lblBattery5";
            this.lblBattery5.Size = new System.Drawing.Size(80, 16);
            this.lblBattery5.TabIndex = 32;
            this.lblBattery5.Text = "Battery 5  -";
            this.lblBattery5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBattery6
            // 
            this.lblBattery6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBattery6.Location = new System.Drawing.Point(24, 424);
            this.lblBattery6.Name = "lblBattery6";
            this.lblBattery6.Size = new System.Drawing.Size(80, 16);
            this.lblBattery6.TabIndex = 33;
            this.lblBattery6.Text = "Battery 6  -";
            this.lblBattery6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBattery7
            // 
            this.lblBattery7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBattery7.Location = new System.Drawing.Point(24, 456);
            this.lblBattery7.Name = "lblBattery7";
            this.lblBattery7.Size = new System.Drawing.Size(80, 16);
            this.lblBattery7.TabIndex = 34;
            this.lblBattery7.Text = "Battery 7  -";
            this.lblBattery7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBattery8
            // 
            this.lblBattery8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBattery8.Location = new System.Drawing.Point(24, 488);
            this.lblBattery8.Name = "lblBattery8";
            this.lblBattery8.Size = new System.Drawing.Size(80, 16);
            this.lblBattery8.TabIndex = 35;
            this.lblBattery8.Text = "Battery 8  -";
            this.lblBattery8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSerNum1
            // 
            this.lblSerNum1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerNum1.ForeColor = System.Drawing.Color.Blue;
            this.lblSerNum1.Location = new System.Drawing.Point(111, 264);
            this.lblSerNum1.Name = "lblSerNum1";
            this.lblSerNum1.Size = new System.Drawing.Size(112, 16);
            this.lblSerNum1.TabIndex = 36;
            this.lblSerNum1.Text = "Serial Number 1";
            this.lblSerNum1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSerNum2
            // 
            this.lblSerNum2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerNum2.ForeColor = System.Drawing.Color.Blue;
            this.lblSerNum2.Location = new System.Drawing.Point(111, 296);
            this.lblSerNum2.Name = "lblSerNum2";
            this.lblSerNum2.Size = new System.Drawing.Size(112, 16);
            this.lblSerNum2.TabIndex = 37;
            this.lblSerNum2.Text = "Serial Number 2";
            this.lblSerNum2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSerNum3
            // 
            this.lblSerNum3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerNum3.ForeColor = System.Drawing.Color.Blue;
            this.lblSerNum3.Location = new System.Drawing.Point(111, 328);
            this.lblSerNum3.Name = "lblSerNum3";
            this.lblSerNum3.Size = new System.Drawing.Size(112, 16);
            this.lblSerNum3.TabIndex = 38;
            this.lblSerNum3.Text = "Serial Number 3";
            this.lblSerNum3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSerNum4
            // 
            this.lblSerNum4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerNum4.ForeColor = System.Drawing.Color.Blue;
            this.lblSerNum4.Location = new System.Drawing.Point(111, 360);
            this.lblSerNum4.Name = "lblSerNum4";
            this.lblSerNum4.Size = new System.Drawing.Size(112, 16);
            this.lblSerNum4.TabIndex = 39;
            this.lblSerNum4.Text = "Serial Number 4";
            this.lblSerNum4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSerNum5
            // 
            this.lblSerNum5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerNum5.ForeColor = System.Drawing.Color.Blue;
            this.lblSerNum5.Location = new System.Drawing.Point(111, 392);
            this.lblSerNum5.Name = "lblSerNum5";
            this.lblSerNum5.Size = new System.Drawing.Size(112, 16);
            this.lblSerNum5.TabIndex = 40;
            this.lblSerNum5.Text = "Serial Number 5";
            this.lblSerNum5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSerNum6
            // 
            this.lblSerNum6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerNum6.ForeColor = System.Drawing.Color.Blue;
            this.lblSerNum6.Location = new System.Drawing.Point(111, 424);
            this.lblSerNum6.Name = "lblSerNum6";
            this.lblSerNum6.Size = new System.Drawing.Size(112, 16);
            this.lblSerNum6.TabIndex = 41;
            this.lblSerNum6.Text = "Serial Number 6";
            this.lblSerNum6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSerNum7
            // 
            this.lblSerNum7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerNum7.ForeColor = System.Drawing.Color.Blue;
            this.lblSerNum7.Location = new System.Drawing.Point(111, 456);
            this.lblSerNum7.Name = "lblSerNum7";
            this.lblSerNum7.Size = new System.Drawing.Size(112, 16);
            this.lblSerNum7.TabIndex = 42;
            this.lblSerNum7.Text = "Serial Number 7";
            this.lblSerNum7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSerNum8
            // 
            this.lblSerNum8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerNum8.ForeColor = System.Drawing.Color.Blue;
            this.lblSerNum8.Location = new System.Drawing.Point(111, 488);
            this.lblSerNum8.Name = "lblSerNum8";
            this.lblSerNum8.Size = new System.Drawing.Size(112, 16);
            this.lblSerNum8.TabIndex = 43;
            this.lblSerNum8.Text = "Serial Number 8";
            this.lblSerNum8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOcVoltage
            // 
            this.lblOcVoltage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOcVoltage.Location = new System.Drawing.Point(379, 216);
            this.lblOcVoltage.Name = "lblOcVoltage";
            this.lblOcVoltage.Size = new System.Drawing.Size(112, 32);
            this.lblOcVoltage.TabIndex = 52;
            this.lblOcVoltage.Text = "Pre-Test Voltage  > 11.0 VDC";
            this.lblOcVoltage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOcV1
            // 
            this.lblOcV1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOcV1.ForeColor = System.Drawing.Color.Blue;
            this.lblOcV1.Location = new System.Drawing.Point(379, 264);
            this.lblOcV1.Name = "lblOcV1";
            this.lblOcV1.Size = new System.Drawing.Size(112, 16);
            this.lblOcV1.TabIndex = 44;
            this.lblOcV1.Text = "OC Voltage 1";
            this.lblOcV1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOcV2
            // 
            this.lblOcV2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOcV2.ForeColor = System.Drawing.Color.Blue;
            this.lblOcV2.Location = new System.Drawing.Point(379, 296);
            this.lblOcV2.Name = "lblOcV2";
            this.lblOcV2.Size = new System.Drawing.Size(112, 16);
            this.lblOcV2.TabIndex = 45;
            this.lblOcV2.Text = "OC Voltage 2";
            this.lblOcV2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOcV3
            // 
            this.lblOcV3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOcV3.ForeColor = System.Drawing.Color.Blue;
            this.lblOcV3.Location = new System.Drawing.Point(379, 328);
            this.lblOcV3.Name = "lblOcV3";
            this.lblOcV3.Size = new System.Drawing.Size(112, 16);
            this.lblOcV3.TabIndex = 46;
            this.lblOcV3.Text = "OC Voltage 3";
            this.lblOcV3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOcV4
            // 
            this.lblOcV4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOcV4.ForeColor = System.Drawing.Color.Blue;
            this.lblOcV4.Location = new System.Drawing.Point(379, 360);
            this.lblOcV4.Name = "lblOcV4";
            this.lblOcV4.Size = new System.Drawing.Size(112, 16);
            this.lblOcV4.TabIndex = 47;
            this.lblOcV4.Text = "OC Voltage 4";
            this.lblOcV4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOcV5
            // 
            this.lblOcV5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOcV5.ForeColor = System.Drawing.Color.Blue;
            this.lblOcV5.Location = new System.Drawing.Point(379, 392);
            this.lblOcV5.Name = "lblOcV5";
            this.lblOcV5.Size = new System.Drawing.Size(112, 16);
            this.lblOcV5.TabIndex = 48;
            this.lblOcV5.Text = "OC Voltage 5";
            this.lblOcV5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOcV6
            // 
            this.lblOcV6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOcV6.ForeColor = System.Drawing.Color.Blue;
            this.lblOcV6.Location = new System.Drawing.Point(379, 424);
            this.lblOcV6.Name = "lblOcV6";
            this.lblOcV6.Size = new System.Drawing.Size(112, 16);
            this.lblOcV6.TabIndex = 49;
            this.lblOcV6.Text = "OC Voltage 6";
            this.lblOcV6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOcV7
            // 
            this.lblOcV7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOcV7.ForeColor = System.Drawing.Color.Blue;
            this.lblOcV7.Location = new System.Drawing.Point(379, 456);
            this.lblOcV7.Name = "lblOcV7";
            this.lblOcV7.Size = new System.Drawing.Size(112, 16);
            this.lblOcV7.TabIndex = 50;
            this.lblOcV7.Text = "OC Voltage 7";
            this.lblOcV7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOcV8
            // 
            this.lblOcV8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOcV8.ForeColor = System.Drawing.Color.Blue;
            this.lblOcV8.Location = new System.Drawing.Point(379, 488);
            this.lblOcV8.Name = "lblOcV8";
            this.lblOcV8.Size = new System.Drawing.Size(112, 16);
            this.lblOcV8.TabIndex = 51;
            this.lblOcV8.Text = "OC Voltage 8";
            this.lblOcV8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoadV
            // 
            this.lblLoadV.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadV.Location = new System.Drawing.Point(513, 216);
            this.lblLoadV.Name = "lblLoadV";
            this.lblLoadV.Size = new System.Drawing.Size(112, 32);
            this.lblLoadV.TabIndex = 61;
            this.lblLoadV.Text = "6A Load Voltage > 9.0 VDC";
            this.lblLoadV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoadV1
            // 
            this.lblLoadV1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadV1.ForeColor = System.Drawing.Color.Blue;
            this.lblLoadV1.Location = new System.Drawing.Point(513, 264);
            this.lblLoadV1.Name = "lblLoadV1";
            this.lblLoadV1.Size = new System.Drawing.Size(112, 16);
            this.lblLoadV1.TabIndex = 53;
            this.lblLoadV1.Text = "Load Voltage 1";
            this.lblLoadV1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoadV2
            // 
            this.lblLoadV2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadV2.ForeColor = System.Drawing.Color.Blue;
            this.lblLoadV2.Location = new System.Drawing.Point(513, 296);
            this.lblLoadV2.Name = "lblLoadV2";
            this.lblLoadV2.Size = new System.Drawing.Size(112, 16);
            this.lblLoadV2.TabIndex = 54;
            this.lblLoadV2.Text = "Load Voltage 2";
            this.lblLoadV2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoadV3
            // 
            this.lblLoadV3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadV3.ForeColor = System.Drawing.Color.Blue;
            this.lblLoadV3.Location = new System.Drawing.Point(513, 328);
            this.lblLoadV3.Name = "lblLoadV3";
            this.lblLoadV3.Size = new System.Drawing.Size(112, 16);
            this.lblLoadV3.TabIndex = 55;
            this.lblLoadV3.Text = "Load Voltage 3";
            this.lblLoadV3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoadV4
            // 
            this.lblLoadV4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadV4.ForeColor = System.Drawing.Color.Blue;
            this.lblLoadV4.Location = new System.Drawing.Point(513, 360);
            this.lblLoadV4.Name = "lblLoadV4";
            this.lblLoadV4.Size = new System.Drawing.Size(112, 16);
            this.lblLoadV4.TabIndex = 56;
            this.lblLoadV4.Text = "Load Voltage 4";
            this.lblLoadV4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoadV5
            // 
            this.lblLoadV5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadV5.ForeColor = System.Drawing.Color.Blue;
            this.lblLoadV5.Location = new System.Drawing.Point(513, 392);
            this.lblLoadV5.Name = "lblLoadV5";
            this.lblLoadV5.Size = new System.Drawing.Size(112, 16);
            this.lblLoadV5.TabIndex = 57;
            this.lblLoadV5.Text = "Load Voltage 5";
            this.lblLoadV5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoadV6
            // 
            this.lblLoadV6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadV6.ForeColor = System.Drawing.Color.Blue;
            this.lblLoadV6.Location = new System.Drawing.Point(513, 424);
            this.lblLoadV6.Name = "lblLoadV6";
            this.lblLoadV6.Size = new System.Drawing.Size(112, 16);
            this.lblLoadV6.TabIndex = 58;
            this.lblLoadV6.Text = "Load Voltage 6";
            this.lblLoadV6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoadV7
            // 
            this.lblLoadV7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadV7.ForeColor = System.Drawing.Color.Blue;
            this.lblLoadV7.Location = new System.Drawing.Point(513, 456);
            this.lblLoadV7.Name = "lblLoadV7";
            this.lblLoadV7.Size = new System.Drawing.Size(112, 16);
            this.lblLoadV7.TabIndex = 59;
            this.lblLoadV7.Text = "Load Voltage 7";
            this.lblLoadV7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoadV8
            // 
            this.lblLoadV8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadV8.ForeColor = System.Drawing.Color.Blue;
            this.lblLoadV8.Location = new System.Drawing.Point(513, 488);
            this.lblLoadV8.Name = "lblLoadV8";
            this.lblLoadV8.Size = new System.Drawing.Size(112, 16);
            this.lblLoadV8.TabIndex = 60;
            this.lblLoadV8.Text = "Load Voltage 8";
            this.lblLoadV8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoLoadV
            // 
            this.lblNoLoadV.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoLoadV.Location = new System.Drawing.Point(647, 216);
            this.lblNoLoadV.Name = "lblNoLoadV";
            this.lblNoLoadV.Size = new System.Drawing.Size(116, 32);
            this.lblNoLoadV.TabIndex = 70;
            this.lblNoLoadV.Text = "Post Load Voltage > 11.0 VDC";
            this.lblNoLoadV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoLoadV1
            // 
            this.lblNoLoadV1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoLoadV1.ForeColor = System.Drawing.Color.Blue;
            this.lblNoLoadV1.Location = new System.Drawing.Point(647, 264);
            this.lblNoLoadV1.Name = "lblNoLoadV1";
            this.lblNoLoadV1.Size = new System.Drawing.Size(112, 16);
            this.lblNoLoadV1.TabIndex = 62;
            this.lblNoLoadV1.Text = "OC Voltage 1";
            this.lblNoLoadV1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoLoadV2
            // 
            this.lblNoLoadV2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoLoadV2.ForeColor = System.Drawing.Color.Blue;
            this.lblNoLoadV2.Location = new System.Drawing.Point(647, 296);
            this.lblNoLoadV2.Name = "lblNoLoadV2";
            this.lblNoLoadV2.Size = new System.Drawing.Size(112, 16);
            this.lblNoLoadV2.TabIndex = 63;
            this.lblNoLoadV2.Text = "OC Voltage 2";
            this.lblNoLoadV2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoLoadV3
            // 
            this.lblNoLoadV3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoLoadV3.ForeColor = System.Drawing.Color.Blue;
            this.lblNoLoadV3.Location = new System.Drawing.Point(647, 328);
            this.lblNoLoadV3.Name = "lblNoLoadV3";
            this.lblNoLoadV3.Size = new System.Drawing.Size(112, 16);
            this.lblNoLoadV3.TabIndex = 64;
            this.lblNoLoadV3.Text = "OC Voltage 3";
            this.lblNoLoadV3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoLoadV4
            // 
            this.lblNoLoadV4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoLoadV4.ForeColor = System.Drawing.Color.Blue;
            this.lblNoLoadV4.Location = new System.Drawing.Point(647, 360);
            this.lblNoLoadV4.Name = "lblNoLoadV4";
            this.lblNoLoadV4.Size = new System.Drawing.Size(112, 16);
            this.lblNoLoadV4.TabIndex = 65;
            this.lblNoLoadV4.Text = "OC Voltage 4";
            this.lblNoLoadV4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoLoadV5
            // 
            this.lblNoLoadV5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoLoadV5.ForeColor = System.Drawing.Color.Blue;
            this.lblNoLoadV5.Location = new System.Drawing.Point(647, 392);
            this.lblNoLoadV5.Name = "lblNoLoadV5";
            this.lblNoLoadV5.Size = new System.Drawing.Size(112, 16);
            this.lblNoLoadV5.TabIndex = 66;
            this.lblNoLoadV5.Text = "OC Voltage 5";
            this.lblNoLoadV5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoLoadV6
            // 
            this.lblNoLoadV6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoLoadV6.ForeColor = System.Drawing.Color.Blue;
            this.lblNoLoadV6.Location = new System.Drawing.Point(647, 424);
            this.lblNoLoadV6.Name = "lblNoLoadV6";
            this.lblNoLoadV6.Size = new System.Drawing.Size(112, 16);
            this.lblNoLoadV6.TabIndex = 67;
            this.lblNoLoadV6.Text = "OC Voltage 6";
            this.lblNoLoadV6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoLoadV7
            // 
            this.lblNoLoadV7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoLoadV7.ForeColor = System.Drawing.Color.Blue;
            this.lblNoLoadV7.Location = new System.Drawing.Point(647, 456);
            this.lblNoLoadV7.Name = "lblNoLoadV7";
            this.lblNoLoadV7.Size = new System.Drawing.Size(112, 16);
            this.lblNoLoadV7.TabIndex = 68;
            this.lblNoLoadV7.Text = "OC Voltage 7";
            this.lblNoLoadV7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoLoadV8
            // 
            this.lblNoLoadV8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoLoadV8.ForeColor = System.Drawing.Color.Blue;
            this.lblNoLoadV8.Location = new System.Drawing.Point(647, 488);
            this.lblNoLoadV8.Name = "lblNoLoadV8";
            this.lblNoLoadV8.Size = new System.Drawing.Size(112, 16);
            this.lblNoLoadV8.TabIndex = 69;
            this.lblNoLoadV8.Text = "OC Voltage 8";
            this.lblNoLoadV8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblID
            // 
            this.lblID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(776, 216);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(90, 32);
            this.lblID.TabIndex = 161;
            this.lblID.Text = "ID Resistor  in Ohms";
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblID1
            // 
            this.lblID1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID1.ForeColor = System.Drawing.Color.Blue;
            this.lblID1.Location = new System.Drawing.Point(776, 264);
            this.lblID1.Name = "lblID1";
            this.lblID1.Size = new System.Drawing.Size(90, 16);
            this.lblID1.TabIndex = 162;
            this.lblID1.Text = "ID Value 1";
            this.lblID1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblID2
            // 
            this.lblID2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID2.ForeColor = System.Drawing.Color.Blue;
            this.lblID2.Location = new System.Drawing.Point(776, 296);
            this.lblID2.Name = "lblID2";
            this.lblID2.Size = new System.Drawing.Size(90, 16);
            this.lblID2.TabIndex = 163;
            this.lblID2.Text = "ID Value 2";
            this.lblID2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblID3
            // 
            this.lblID3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID3.ForeColor = System.Drawing.Color.Blue;
            this.lblID3.Location = new System.Drawing.Point(776, 328);
            this.lblID3.Name = "lblID3";
            this.lblID3.Size = new System.Drawing.Size(90, 16);
            this.lblID3.TabIndex = 164;
            this.lblID3.Text = "ID Value 3";
            this.lblID3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblID4
            // 
            this.lblID4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID4.ForeColor = System.Drawing.Color.Blue;
            this.lblID4.Location = new System.Drawing.Point(776, 360);
            this.lblID4.Name = "lblID4";
            this.lblID4.Size = new System.Drawing.Size(90, 16);
            this.lblID4.TabIndex = 165;
            this.lblID4.Text = "ID Value 4";
            this.lblID4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblID5
            // 
            this.lblID5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID5.ForeColor = System.Drawing.Color.Blue;
            this.lblID5.Location = new System.Drawing.Point(776, 392);
            this.lblID5.Name = "lblID5";
            this.lblID5.Size = new System.Drawing.Size(90, 16);
            this.lblID5.TabIndex = 166;
            this.lblID5.Text = "ID Value 5";
            this.lblID5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblID6
            // 
            this.lblID6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID6.ForeColor = System.Drawing.Color.Blue;
            this.lblID6.Location = new System.Drawing.Point(776, 424);
            this.lblID6.Name = "lblID6";
            this.lblID6.Size = new System.Drawing.Size(90, 16);
            this.lblID6.TabIndex = 167;
            this.lblID6.Text = "ID Value 6";
            this.lblID6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblID7
            // 
            this.lblID7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID7.ForeColor = System.Drawing.Color.Blue;
            this.lblID7.Location = new System.Drawing.Point(776, 456);
            this.lblID7.Name = "lblID7";
            this.lblID7.Size = new System.Drawing.Size(90, 16);
            this.lblID7.TabIndex = 168;
            this.lblID7.Text = "ID Value 7";
            this.lblID7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblID8
            // 
            this.lblID8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID8.ForeColor = System.Drawing.Color.Blue;
            this.lblID8.Location = new System.Drawing.Point(776, 488);
            this.lblID8.Name = "lblID8";
            this.lblID8.Size = new System.Drawing.Size(90, 16);
            this.lblID8.TabIndex = 169;
            this.lblID8.Text = "ID Value 8";
            this.lblID8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResult
            // 
            this.lblResult.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(880, 221);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(90, 23);
            this.lblResult.TabIndex = 79;
            this.lblResult.Text = "Results";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResult1
            // 
            this.lblResult1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult1.ForeColor = System.Drawing.Color.Blue;
            this.lblResult1.Location = new System.Drawing.Point(880, 264);
            this.lblResult1.Name = "lblResult1";
            this.lblResult1.Size = new System.Drawing.Size(90, 16);
            this.lblResult1.TabIndex = 71;
            this.lblResult1.Text = "Result 1";
            this.lblResult1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResult2
            // 
            this.lblResult2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult2.ForeColor = System.Drawing.Color.Blue;
            this.lblResult2.Location = new System.Drawing.Point(880, 296);
            this.lblResult2.Name = "lblResult2";
            this.lblResult2.Size = new System.Drawing.Size(90, 16);
            this.lblResult2.TabIndex = 72;
            this.lblResult2.Text = "Result 2";
            this.lblResult2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResult3
            // 
            this.lblResult3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult3.ForeColor = System.Drawing.Color.Blue;
            this.lblResult3.Location = new System.Drawing.Point(880, 328);
            this.lblResult3.Name = "lblResult3";
            this.lblResult3.Size = new System.Drawing.Size(90, 16);
            this.lblResult3.TabIndex = 73;
            this.lblResult3.Text = "Result 3";
            this.lblResult3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResult4
            // 
            this.lblResult4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult4.ForeColor = System.Drawing.Color.Blue;
            this.lblResult4.Location = new System.Drawing.Point(880, 360);
            this.lblResult4.Name = "lblResult4";
            this.lblResult4.Size = new System.Drawing.Size(90, 16);
            this.lblResult4.TabIndex = 74;
            this.lblResult4.Text = "Result 4";
            this.lblResult4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResult5
            // 
            this.lblResult5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult5.ForeColor = System.Drawing.Color.Blue;
            this.lblResult5.Location = new System.Drawing.Point(880, 392);
            this.lblResult5.Name = "lblResult5";
            this.lblResult5.Size = new System.Drawing.Size(90, 16);
            this.lblResult5.TabIndex = 75;
            this.lblResult5.Text = "Result 5";
            this.lblResult5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResult6
            // 
            this.lblResult6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult6.ForeColor = System.Drawing.Color.Blue;
            this.lblResult6.Location = new System.Drawing.Point(880, 424);
            this.lblResult6.Name = "lblResult6";
            this.lblResult6.Size = new System.Drawing.Size(90, 16);
            this.lblResult6.TabIndex = 76;
            this.lblResult6.Text = "Result 6";
            this.lblResult6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResult7
            // 
            this.lblResult7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult7.ForeColor = System.Drawing.Color.Blue;
            this.lblResult7.Location = new System.Drawing.Point(880, 456);
            this.lblResult7.Name = "lblResult7";
            this.lblResult7.Size = new System.Drawing.Size(90, 16);
            this.lblResult7.TabIndex = 77;
            this.lblResult7.Text = "Result 7";
            this.lblResult7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResult8
            // 
            this.lblResult8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult8.ForeColor = System.Drawing.Color.Blue;
            this.lblResult8.Location = new System.Drawing.Point(880, 488);
            this.lblResult8.Name = "lblResult8";
            this.lblResult8.Size = new System.Drawing.Size(90, 16);
            this.lblResult8.TabIndex = 78;
            this.lblResult8.Text = "Result 8";
            this.lblResult8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEnterSerNum
            // 
            this.lblEnterSerNum.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnterSerNum.Location = new System.Drawing.Point(16, 184);
            this.lblEnterSerNum.Name = "lblEnterSerNum";
            this.lblEnterSerNum.Size = new System.Drawing.Size(200, 16);
            this.lblEnterSerNum.TabIndex = 80;
            this.lblEnterSerNum.Text = "Enter Starting Serial Number  - ";
            this.lblEnterSerNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartSerNum
            // 
            this.txtStartSerNum.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartSerNum.Location = new System.Drawing.Point(219, 181);
            this.txtStartSerNum.MaxLength = 40;
            this.txtStartSerNum.Name = "txtStartSerNum";
            this.txtStartSerNum.Size = new System.Drawing.Size(279, 22);
            this.txtStartSerNum.TabIndex = 5;
            this.txtStartSerNum.Text = "(01)00847946016838  (21)AV11150001";
            this.txtStartSerNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtStartSerNum.TextChanged += new System.EventHandler(this.txtStartSerNum_TextChanged);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(504, 181);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(149, 23);
            this.btnAccept.TabIndex = 6;
            this.btnAccept.Text = "Accept Serial Number";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(96, 528);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(80, 23);
            this.btnStart.TabIndex = 16;
            this.btnStart.Text = "Start Test";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(95, 528);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 23);
            this.btnStop.TabIndex = 17;
            this.btnStop.Text = "Stop Test";
            this.btnStop.Visible = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblElapsed
            // 
            this.lblElapsed.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElapsed.Location = new System.Drawing.Point(376, 528);
            this.lblElapsed.Name = "lblElapsed";
            this.lblElapsed.Size = new System.Drawing.Size(140, 16);
            this.lblElapsed.TabIndex = 85;
            this.lblElapsed.Text = "Elapsed Test Time  -";
            this.lblElapsed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblElapsedTime
            // 
            this.lblElapsedTime.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElapsedTime.ForeColor = System.Drawing.Color.Blue;
            this.lblElapsedTime.Location = new System.Drawing.Point(528, 528);
            this.lblElapsedTime.Name = "lblElapsedTime";
            this.lblElapsedTime.Size = new System.Drawing.Size(144, 16);
            this.lblElapsedTime.TabIndex = 86;
            this.lblElapsedTime.Text = "Elapsed Time Display";
            this.lblElapsedTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStartDateTime
            // 
            this.lblStartDateTime.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDateTime.ForeColor = System.Drawing.Color.Blue;
            this.lblStartDateTime.Location = new System.Drawing.Point(528, 552);
            this.lblStartDateTime.Name = "lblStartDateTime";
            this.lblStartDateTime.Size = new System.Drawing.Size(160, 16);
            this.lblStartDateTime.TabIndex = 87;
            this.lblStartDateTime.Text = "Date Time Display";
            this.lblStartDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStopDateTime
            // 
            this.lblStopDateTime.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStopDateTime.ForeColor = System.Drawing.Color.Blue;
            this.lblStopDateTime.Location = new System.Drawing.Point(528, 576);
            this.lblStopDateTime.Name = "lblStopDateTime";
            this.lblStopDateTime.Size = new System.Drawing.Size(160, 16);
            this.lblStopDateTime.TabIndex = 88;
            this.lblStopDateTime.Text = "Date Time Display";
            this.lblStopDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(40, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(176, 56);
            this.pictureBox1.TabIndex = 89;
            this.pictureBox1.TabStop = false;
            // 
            // lblTestTitle
            // 
            this.lblTestTitle.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestTitle.Location = new System.Drawing.Point(249, 41);
            this.lblTestTitle.Name = "lblTestTitle";
            this.lblTestTitle.Size = new System.Drawing.Size(116, 23);
            this.lblTestTitle.TabIndex = 90;
            this.lblTestTitle.Text = "Enter Test Title";
            this.lblTestTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(96, 576);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(80, 23);
            this.btnQuit.TabIndex = 18;
            this.btnQuit.Text = "Quit";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // lblStartTime
            // 
            this.lblStartTime.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartTime.Location = new System.Drawing.Point(376, 552);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(140, 16);
            this.lblStartTime.TabIndex = 97;
            this.lblStartTime.Text = "Test Starting Time -";
            this.lblStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTestComplete
            // 
            this.lblTestComplete.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestComplete.Location = new System.Drawing.Point(356, 576);
            this.lblTestComplete.Name = "lblTestComplete";
            this.lblTestComplete.Size = new System.Drawing.Size(160, 16);
            this.lblTestComplete.TabIndex = 98;
            this.lblTestComplete.Text = "Test Completition Time -";
            this.lblTestComplete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLotNum
            // 
            this.txtLotNum.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotNum.Location = new System.Drawing.Point(688, 109);
            this.txtLotNum.MaxLength = 15;
            this.txtLotNum.Name = "txtLotNum";
            this.txtLotNum.Size = new System.Drawing.Size(120, 22);
            this.txtLotNum.TabIndex = 2;
            this.txtLotNum.Text = "Lot Number";
            // 
            // lblLotNum
            // 
            this.lblLotNum.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLotNum.Location = new System.Drawing.Point(536, 112);
            this.lblLotNum.Name = "lblLotNum";
            this.lblLotNum.Size = new System.Drawing.Size(136, 16);
            this.lblLotNum.TabIndex = 99;
            this.lblLotNum.Text = "Enter lot number -";
            this.lblLotNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFileName
            // 
            this.lblFileName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.Location = new System.Drawing.Point(240, 148);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(232, 16);
            this.lblFileName.TabIndex = 102;
            this.lblFileName.Text = "Database file name";
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(96, 145);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(128, 23);
            this.btnSelectFile.TabIndex = 3;
            this.btnSelectFile.Text = "Select Output File";
            this.btnSelectFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtPoNumbr
            // 
            this.txtPoNumbr.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPoNumbr.Location = new System.Drawing.Point(240, 109);
            this.txtPoNumbr.MaxLength = 15;
            this.txtPoNumbr.Name = "txtPoNumbr";
            this.txtPoNumbr.Size = new System.Drawing.Size(120, 22);
            this.txtPoNumbr.TabIndex = 1;
            this.txtPoNumbr.Text = "PO Number";
            // 
            // lblPoNumbr
            // 
            this.lblPoNumbr.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoNumbr.Location = new System.Drawing.Point(8, 112);
            this.lblPoNumbr.Name = "lblPoNumbr";
            this.lblPoNumbr.Size = new System.Drawing.Size(216, 16);
            this.lblPoNumbr.TabIndex = 103;
            this.lblPoNumbr.Text = "Enter purchase order number -";
            this.lblPoNumbr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCellCode
            // 
            this.txtCellCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCellCode.Location = new System.Drawing.Point(688, 145);
            this.txtCellCode.MaxLength = 15;
            this.txtCellCode.Name = "txtCellCode";
            this.txtCellCode.Size = new System.Drawing.Size(120, 22);
            this.txtCellCode.TabIndex = 4;
            this.txtCellCode.Text = "Cell Date Code";
            // 
            // lblCellCode
            // 
            this.lblCellCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCellCode.Location = new System.Drawing.Point(528, 148);
            this.lblCellCode.Name = "lblCellCode";
            this.lblCellCode.Size = new System.Drawing.Size(144, 16);
            this.lblCellCode.TabIndex = 105;
            this.lblCellCode.Text = "Enter Cell Date Code -";
            this.lblCellCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPackSerNums
            // 
            this.lblPackSerNums.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPackSerNums.Location = new System.Drawing.Point(245, 216);
            this.lblPackSerNums.Name = "lblPackSerNums";
            this.lblPackSerNums.Size = new System.Drawing.Size(112, 32);
            this.lblPackSerNums.TabIndex = 107;
            this.lblPackSerNums.Text = "Board Serial Number";
            this.lblPackSerNums.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPackSerNum1
            // 
            this.txtPackSerNum1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackSerNum1.Location = new System.Drawing.Point(245, 261);
            this.txtPackSerNum1.MaxLength = 40;
            this.txtPackSerNum1.Name = "txtPackSerNum1";
            this.txtPackSerNum1.Size = new System.Drawing.Size(112, 22);
            this.txtPackSerNum1.TabIndex = 7;
            this.txtPackSerNum1.Text = "AV01050001";
            this.txtPackSerNum1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPackSerNum2
            // 
            this.txtPackSerNum2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackSerNum2.Location = new System.Drawing.Point(245, 293);
            this.txtPackSerNum2.MaxLength = 15;
            this.txtPackSerNum2.Name = "txtPackSerNum2";
            this.txtPackSerNum2.Size = new System.Drawing.Size(112, 22);
            this.txtPackSerNum2.TabIndex = 8;
            this.txtPackSerNum2.Text = "AV01050001";
            this.txtPackSerNum2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPackSerNum2.TextChanged += new System.EventHandler(this.txtPackSerNum2_TextChanged);
            // 
            // txtPackSerNum3
            // 
            this.txtPackSerNum3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackSerNum3.Location = new System.Drawing.Point(245, 325);
            this.txtPackSerNum3.MaxLength = 15;
            this.txtPackSerNum3.Name = "txtPackSerNum3";
            this.txtPackSerNum3.Size = new System.Drawing.Size(112, 22);
            this.txtPackSerNum3.TabIndex = 9;
            this.txtPackSerNum3.Text = "AV01050001";
            this.txtPackSerNum3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPackSerNum4
            // 
            this.txtPackSerNum4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackSerNum4.Location = new System.Drawing.Point(245, 357);
            this.txtPackSerNum4.MaxLength = 15;
            this.txtPackSerNum4.Name = "txtPackSerNum4";
            this.txtPackSerNum4.Size = new System.Drawing.Size(112, 22);
            this.txtPackSerNum4.TabIndex = 10;
            this.txtPackSerNum4.Text = "AV01050001";
            this.txtPackSerNum4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPackSerNum5
            // 
            this.txtPackSerNum5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackSerNum5.Location = new System.Drawing.Point(245, 389);
            this.txtPackSerNum5.MaxLength = 15;
            this.txtPackSerNum5.Name = "txtPackSerNum5";
            this.txtPackSerNum5.Size = new System.Drawing.Size(112, 22);
            this.txtPackSerNum5.TabIndex = 11;
            this.txtPackSerNum5.Text = "AV01050001";
            this.txtPackSerNum5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPackSerNum6
            // 
            this.txtPackSerNum6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackSerNum6.Location = new System.Drawing.Point(245, 421);
            this.txtPackSerNum6.MaxLength = 15;
            this.txtPackSerNum6.Name = "txtPackSerNum6";
            this.txtPackSerNum6.Size = new System.Drawing.Size(112, 22);
            this.txtPackSerNum6.TabIndex = 12;
            this.txtPackSerNum6.Text = "AV01050001";
            this.txtPackSerNum6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPackSerNum7
            // 
            this.txtPackSerNum7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackSerNum7.Location = new System.Drawing.Point(245, 453);
            this.txtPackSerNum7.MaxLength = 15;
            this.txtPackSerNum7.Name = "txtPackSerNum7";
            this.txtPackSerNum7.Size = new System.Drawing.Size(112, 22);
            this.txtPackSerNum7.TabIndex = 13;
            this.txtPackSerNum7.Text = "AV01050001";
            this.txtPackSerNum7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPackSerNum8
            // 
            this.txtPackSerNum8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackSerNum8.Location = new System.Drawing.Point(245, 485);
            this.txtPackSerNum8.MaxLength = 15;
            this.txtPackSerNum8.Name = "txtPackSerNum8";
            this.txtPackSerNum8.Size = new System.Drawing.Size(112, 22);
            this.txtPackSerNum8.TabIndex = 14;
            this.txtPackSerNum8.Text = "AV01050001";
            this.txtPackSerNum8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkIDRes
            // 
            this.chkIDRes.Location = new System.Drawing.Point(666, 181);
            this.chkIDRes.Name = "chkIDRes";
            this.chkIDRes.Size = new System.Drawing.Size(128, 23);
            this.chkIDRes.TabIndex = 15;
            this.chkIDRes.Text = "Run ID Pin Test";
            this.chkIDRes.CheckedChanged += new System.EventHandler(this.chkIDRes_CheckedChanged);
            // 
            // chkIDResOnly
            // 
            this.chkIDResOnly.Enabled = false;
            this.chkIDResOnly.Location = new System.Drawing.Point(805, 181);
            this.chkIDResOnly.Name = "chkIDResOnly";
            this.chkIDResOnly.Size = new System.Drawing.Size(160, 23);
            this.chkIDResOnly.TabIndex = 170;
            this.chkIDResOnly.Text = "Only Run ID Pin Test";
            // 
            // txtTestTitle
            // 
            this.txtTestTitle.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTestTitle.Location = new System.Drawing.Point(379, 39);
            this.txtTestTitle.Name = "txtTestTitle";
            this.txtTestTitle.Size = new System.Drawing.Size(487, 27);
            this.txtTestTitle.TabIndex = 0;
            this.txtTestTitle.Text = "1008-1003-01 Battery Pack Test";
            this.txtTestTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTestStatus
            // 
            this.lblTestStatus.AutoSize = true;
            this.lblTestStatus.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestStatus.Location = new System.Drawing.Point(210, 532);
            this.lblTestStatus.Name = "lblTestStatus";
            this.lblTestStatus.Size = new System.Drawing.Size(135, 18);
            this.lblTestStatus.TabIndex = 171;
            this.lblTestStatus.Text = "Test Complete";
            this.lblTestStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTestStatus.Click += new System.EventHandler(this.lblTestStatus_Click);
            // 
            // Zoll_2015
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
            this.ClientSize = new System.Drawing.Size(984, 635);
            this.Controls.Add(this.lblTestStatus);
            this.Controls.Add(this.txtTestTitle);
            this.Controls.Add(this.chkIDResOnly);
            this.Controls.Add(this.lblID8);
            this.Controls.Add(this.lblID7);
            this.Controls.Add(this.lblID6);
            this.Controls.Add(this.lblID5);
            this.Controls.Add(this.lblID4);
            this.Controls.Add(this.lblID3);
            this.Controls.Add(this.lblID2);
            this.Controls.Add(this.lblID1);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.chkIDRes);
            this.Controls.Add(this.txtPackSerNum8);
            this.Controls.Add(this.txtPackSerNum7);
            this.Controls.Add(this.txtPackSerNum6);
            this.Controls.Add(this.txtPackSerNum5);
            this.Controls.Add(this.txtPackSerNum4);
            this.Controls.Add(this.txtPackSerNum3);
            this.Controls.Add(this.txtPackSerNum2);
            this.Controls.Add(this.txtPackSerNum1);
            this.Controls.Add(this.lblPackSerNums);
            this.Controls.Add(this.txtCellCode);
            this.Controls.Add(this.lblCellCode);
            this.Controls.Add(this.txtPoNumbr);
            this.Controls.Add(this.lblPoNumbr);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.txtLotNum);
            this.Controls.Add(this.lblLotNum);
            this.Controls.Add(this.lblTestComplete);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.lblTestTitle);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblStopDateTime);
            this.Controls.Add(this.lblStartDateTime);
            this.Controls.Add(this.lblElapsedTime);
            this.Controls.Add(this.lblElapsed);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.txtStartSerNum);
            this.Controls.Add(this.lblEnterSerNum);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblResult8);
            this.Controls.Add(this.lblResult7);
            this.Controls.Add(this.lblResult6);
            this.Controls.Add(this.lblResult5);
            this.Controls.Add(this.lblResult4);
            this.Controls.Add(this.lblResult3);
            this.Controls.Add(this.lblResult2);
            this.Controls.Add(this.lblResult1);
            this.Controls.Add(this.lblNoLoadV);
            this.Controls.Add(this.lblNoLoadV8);
            this.Controls.Add(this.lblNoLoadV7);
            this.Controls.Add(this.lblNoLoadV6);
            this.Controls.Add(this.lblNoLoadV5);
            this.Controls.Add(this.lblNoLoadV4);
            this.Controls.Add(this.lblNoLoadV3);
            this.Controls.Add(this.lblNoLoadV2);
            this.Controls.Add(this.lblNoLoadV1);
            this.Controls.Add(this.lblLoadV);
            this.Controls.Add(this.lblLoadV8);
            this.Controls.Add(this.lblLoadV7);
            this.Controls.Add(this.lblLoadV6);
            this.Controls.Add(this.lblLoadV5);
            this.Controls.Add(this.lblLoadV4);
            this.Controls.Add(this.lblLoadV3);
            this.Controls.Add(this.lblLoadV2);
            this.Controls.Add(this.lblLoadV1);
            this.Controls.Add(this.lblOcVoltage);
            this.Controls.Add(this.lblOcV8);
            this.Controls.Add(this.lblOcV7);
            this.Controls.Add(this.lblOcV6);
            this.Controls.Add(this.lblOcV5);
            this.Controls.Add(this.lblOcV4);
            this.Controls.Add(this.lblOcV3);
            this.Controls.Add(this.lblOcV2);
            this.Controls.Add(this.lblOcV1);
            this.Controls.Add(this.lblSerNum8);
            this.Controls.Add(this.lblSerNum7);
            this.Controls.Add(this.lblSerNum6);
            this.Controls.Add(this.lblSerNum5);
            this.Controls.Add(this.lblSerNum4);
            this.Controls.Add(this.lblSerNum3);
            this.Controls.Add(this.lblSerNum2);
            this.Controls.Add(this.lblSerNum1);
            this.Controls.Add(this.lblBattery8);
            this.Controls.Add(this.lblBattery7);
            this.Controls.Add(this.lblBattery6);
            this.Controls.Add(this.lblBattery5);
            this.Controls.Add(this.lblBattery4);
            this.Controls.Add(this.lblBattery3);
            this.Controls.Add(this.lblBattery2);
            this.Controls.Add(this.lblSerialNums);
            this.Controls.Add(this.lblBattery1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Zoll_2015";
            this.Text = "Zoll_2015";
            this.Load += new System.EventHandler(this.Zoll_2015_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Zoll_2015());
		}

		/// <summary>
		/// Come here when the Accept serial number button is pushed and set up the serial numbers.
		/// </summary>
		private void btnAccept_Click(object sender, System.EventArgs e)
		{
			int i;
            int StartSerNumTxtLen;
			double Test;
			ushort Bit = 1;

			if(txtStartSerNum.TextLength < 10)			// A valid serial number has atleast 10 chars - rwb 5/26/2015
			{
				MessageBox.Show("Not a valid serial number.", "Serial Number Entry Error",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			StartSerNumTxt = txtStartSerNum.Text;		// Grab string from message box
            StartSerNumTxtLen = StartSerNumTxt.Length;  // Find how long the serial number is - rwb 5/26/2015
            WeekYearTxt = StartSerNumTxt.Remove(StartSerNumTxtLen - 4, 4);	// Isolate week/year string - rwb 5/26/2015
            SerNumTxt = StartSerNumTxt.Remove(0, StartSerNumTxtLen - 4);		// Isolate ser num string - rwb 5/26/2015
			StartSerialNum = int.Parse(SerNumTxt);		// Convert the string to int

			TestMask = 0;		// Clear the test mask
			ClearLabels(true);	// Clear all the old stuff
			ClearAllRelays();	// Clear the relay indicators

			// Do a quick scan to see which channels are active.

			SaveEngUnits = PreTestVtg;
			ScanChannels1(0,3,false,4);
			ScanChannels2(0,3,false,4);


			for (i=0; i < MaxChan; ++i)		// Look for channels with batteries connected
			{
				Test = Math.Abs(SaveEngUnits[i]);
				if (Test < Threshold)
				{
					TestChannel[i] = false;
					txtPackSerNum[i].Enabled = false;	// Don't show a pack number box
					txtPackSerNum[i].Visible = false;	// if nothing to test
				}
				else
				{
					TestChannel[i] = true;
					txtPackSerNum[i].Enabled = true;	// Show a pack number box
					txtPackSerNum[i].Visible = true;	// if something to test
					strMechanicalFit[i] = "Pass";		// Set Pass if pack connected

					strPackSerNum[i] = WeekYearTxt + StartSerialNum.ToString("D4");     //rwb 5/27/2015
                    lblSerialNum[i].Text = WeekYearTxt.Remove(0, StartSerNumTxtLen - 10) + StartSerialNum.ToString("D4");    //rwb 5/27/2015
                    StartSerialNum++;
					NextStartSerialNum = StartSerialNum;
					TestMask = (ushort)(TestMask | Bit);		// Turn on the mask bit
				}

				Bit = (ushort)(Bit << 1);		// Shift the bit over
			}

			GotASerialNum = true;		// Indicate that a serial number was accepted

		}	// End of btnAccept_Click(object sender, System.EventArgs e)

		/// <summary>
		/// Come here when the Stop button is pushed and do everything needed to stop the test.
		/// </summary>
		private void btnStop_Click(object sender, System.EventArgs e)
		{
			btnStart.Enabled = true;
			btnStart.Visible = true;
			btnStop.Enabled = false;
			btnStop.Visible = false;

			timer1.Enabled = false;	// Stop timer

			ClearAllRelays();

		}	// End of btnStop_Click(object sender, System.EventArgs e)

		/// <summary>
		/// Come here when the Start button is pushed and do everything needed to start the test.
		/// </summary>
		private void btnStart_Click(object sender, System.EventArgs e)
		{
			int i;
			int j;

			// Check some basic stuff to see if we're ready to test

			if(txtPoNumbr.Text == "")	// Look for a purchase order number
			{
				MessageBox.Show("You must enter a purchase order number.", "Purchase Order Number Entry Error",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if(txtLotNum.Text == "")	// Look for a lot number
			{
				MessageBox.Show("You must enter a lot number.", "Lot Number Entry Error",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if(lblFileName.Text == "")	// Look for a file name
			{
				MessageBox.Show("You must enter file name.", "File Name Entry Error",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if(txtCellCode.Text == "")	// Look for a cell date code
			{
				MessageBox.Show("You must enter a cell date code.", "Cell Date Code Entry Error",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if(GotASerialNum == false)	// Did we ever select a serial number
			{
				MessageBox.Show("You must accept a serial number.", "Serial Number Entry Error",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if(TestMask == 0)	// Do we have anything to test
			{
				MessageBox.Show("There are no valid test units connected.", "Nothing to test Error",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			for (i=0; i < MaxChan; ++i)		// Make sure channels have valid board serial nums
			{
				if (TestChannel[i] == true)
				{
					if(txtPackSerNum[i].Text == "")
					{
						j = i + 1;
						MessageBox.Show("You must enter a serial number for Battery " + j.ToString(), "Board Serial Number Entry Error",
							MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
			}

			btnStart.Enabled = false;
			btnStart.Visible = false;
			btnStop.Enabled = true;
			btnStop.Visible = true;

			//
			// Setup the test for 53 seconds.
			//

			counter = -3;
			elapsedTimeCtr = -3;
			lblShowData =  lblPreTestVtg;
			SaveEngUnits = PreTestVtg;

			timer1.Interval = 1000;	// 1 sec intervals
			timer1.Enabled = true;	// Enable timer
			lblStartDateTime.Text = DateTime.Now.ToString();

			PassMask = 0;	// Clear the mask bits
			FailMask = 0;

		}	// End of btnStart_Click(object sender, System.EventArgs e)

		/// <summary>
		/// This method does a single scan of the channels and fills the data buffer
		/// </summary>
		private void ScanChannels1(int LowChan, int HighChan, bool ShowData, int NumChannels)
		{
			MccDaq.ErrorInfo ULStat1;
			MccDaq.Range Range;
			MccDaq.ScanOptions Options;

			int i;
			int j;
			int Rate;
			int Count;
			int Offset = 0;
			float EngUnits;

			//  Collect the values by calling MccDaq.MccBoard.AInScan function
			//  Parameters:
			//    LowChan    :the first channel of the scan
			//    HighChan   :the last channel of the scan
			//    Count      :the total number of A/D samples to collect
			//    Rate       :sample rate
			//    Range      :the range for the board
			//    MemHandle  :Handle for Windows buffer to store data in
			//    Options    :data collection options

			Count = 4;				//  total number of data points to collect
			Rate = 1000;			//  per channel sampling rate ((samples per second) per channel)


			if(Board1SerialNum == 1)	// Serial num 1 is first miniLAb
				Offset = 0;

			if(Board1SerialNum == 2)	// Serial num 2 is second miniLab
				Offset = 4;

			//  return data as 12-bit values (ignored for 16-bit boards)
			Options = MccDaq.ScanOptions.ConvertData;
			Range = MccDaq.Range.Bip20Volts; // set the range
 
			ULStat1 = DaqBoard1.AInScan( LowChan, HighChan, Count, ref Rate, Range, MemHandle1, Options);

			if (ULStat1.Value == MccDaq.ErrorInfo.ErrorCode.BadRange)
			{
				MessageBox.Show( "Change the Range argument to one supported by this board.", "Unsupported Range", 0);
			}
   
			//  Transfer the data from the memory buffer set up by Windows to an array
			ULStat1 = MccDaq.MccService.WinBufToArray( MemHandle1, out ADData[0], FirstPoint, Count);

			for (i=0; i < NumChannels; ++i)
			{
				j = i + Offset;

				//  Convert raw data to Volts by calling ToEngUnits (member function of MccBoard class)
				ULStat1 = DaqBoard1.ToEngUnits( Range, ADData[i], out EngUnits);
				SaveEngUnits[j] = EngUnits;		//Save the Voltage.

				if (ShowData)					//See if we want to show the data
					if (TestChannel[j])
						lblShowData[j].Text = EngUnits.ToString("F2") + " Volts"; //Show the voltage.
			}

		}	// End of ScanChannels1(int LowChan, int HighChan, bool ShowData, int NumChannels)

		/// <summary>
		/// This method does a single scan of the channels and fills the data buffer
		/// </summary>
		private void ScanChannels2(int LowChan, int HighChan, bool ShowData, int NumChannels)
		{
			MccDaq.ErrorInfo ULStat2;
			MccDaq.Range Range;
			MccDaq.ScanOptions Options;

			int i;
			int j;
			int Rate;
			int Count;
			int Offset = 0;
			float EngUnits;


			//  Collect the values by calling MccDaq.MccBoard.AInScan function
			//  Parameters:
			//    LowChan    :the first channel of the scan
			//    HighChan   :the last channel of the scan
			//    Count      :the total number of A/D samples to collect
			//    Rate       :sample rate
			//    Range      :the range for the board
			//    MemHandle  :Handle for Windows buffer to store data in
			//    Options    :data collection options

			Count = 4;				//  total number of data points to collect
			Rate = 1000;			//  per channel sampling rate ((samples per second) per channel)

			if(Board2SerialNum == 1)	// Serial num 1 is first miniLAb
				Offset = 0;

			if(Board2SerialNum == 2)	// Serial num 2 is second miniLab
				Offset = 4;

			//  return data as 12-bit values (ignored for 16-bit boards)
			Options = MccDaq.ScanOptions.ConvertData;
			Range = MccDaq.Range.Bip20Volts; // set the range

			ULStat2 = DaqBoard2.AInScan( LowChan, HighChan, Count, ref Rate, Range, MemHandle2, Options);

			if (ULStat2.Value == MccDaq.ErrorInfo.ErrorCode.BadRange)
			{
				MessageBox.Show( "Change the Range argument to one supported by this board.", "Unsupported Range", 0);
			}
   
			//  Transfer the data from the memory buffer set up by Windows to an array
			ULStat2 = MccDaq.MccService.WinBufToArray( MemHandle2, out ADData[0], FirstPoint, Count);

			for (i=0; i < NumChannels; ++i)
			{
				j = i + Offset;

				//  Convert raw data to Volts by calling ToEngUnits (member function of MccBoard class)
				ULStat2 = DaqBoard2.ToEngUnits( Range, ADData[i], out EngUnits);
				SaveEngUnits[j] = EngUnits;		//Save the Voltage.

				if (ShowData)					//See if we want to show the data
					if (TestChannel[j])
						lblShowData[j].Text = EngUnits.ToString("F2") + " Volts"; //Show the voltage.
			}

		}	// End of ScanChannels2(int LowChan, int HighChan, bool ShowData, int NumChannels)

		/// <summary>
		/// This method processes the timer tick. The test runs here.
		/// </summary>
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			int i;
			ushort Bit;

            lblTestStatus.Text = "Test Started";    //rwb 5/27/2015

			counter = counter + 1; 
			elapsedTimeCtr = elapsedTimeCtr + 1;
			lblElapsedTime.Text = elapsedTimeCtr.ToString("F2") + " seconds";
			lblStopDateTime.Text = DateTime.Now.ToString();

			switch (counter)
			{
				case -1:

					DoIDTest();

					break;
				case 0:
					lblShowData =  lblLoadVtg;
					SaveEngUnits = LoadVtg;
					SetRelays(PortNumA, TestMask);	// Apply the loads
					break;

				case 20:
					lblShowData =  lblNoLoadVtg;
					SaveEngUnits = NoLoadVtg;
					SetRelays(PortNumA, 0);			// Remove the loads
					break;

				case 50:
					timer1.Enabled = false;		// Stop timer

					lblShowData =  lblResults;

					PassMask = 0;	// Clear the masks
					FailMask = 0;

					Bit = 1;	// Initialize the shift bit

					for (i=0; i < MaxChan; ++i)
					{
						if (!TestChannel[i])
						{
							Bit = (ushort)(Bit << 1);		// Shift the bit over
							lblShowData[i].Text = "Not Tested";
							continue;	//Continue if the channel isn't being tested
						}

						if (LoadVtg[i] > 9.0)
						{
							if (NoLoadVtg[i] > 11.0)
							{
								lblShowData[i].Text = "Pass";
								PassMask = (ushort)(PassMask | Bit);	// Turn on the pass mask bit
							}
						}
						else
						{
							lblShowData[i].Text = "Fail";
							FailMask = (ushort)(FailMask | Bit);		// Turn on the fail mask bit
						}

						Bit = (ushort)(Bit << 1);		// Shift the bit over
					}

					btnStart.Enabled = true;	// Reset the button to "Start"
					btnStart.Visible = true;
					btnStop.Enabled = false;
					btnStop.Visible = false;

					txtStartSerNum.Text = WeekYearTxt + NextStartSerialNum.ToString("D4");	// Update the starting serial number.

					// We need to separate the fail mask into a low port and high port value

					FailMaskHigh = FailMask;	// Get the mask and shift it left 4 bits
					FailMaskHigh = (ushort)(FailMaskHigh >> 4);

					FailMaskLow = FailMask;		// Get the mask and strip the upper bits
					FailMaskLow = (ushort)(FailMaskLow & 15);

					SetRelays(PortNumCL, FailMaskLow);	// Show who failed
					SetRelays(PortNumCH, FailMaskHigh);	// Show who failed

					if (chkIDResOnly.Checked == false)
					{
						GotASerialNum = false;	// Force a new serial number select
					}

					for (i=0; i < MaxChan; ++i)	// Run this loop to save all the data
					{
//						if (!TestChannel[i]) continue;	//Continue if the channel isn't being tested
						if (txtPackSerNum[i].Text == "")
						{
							txtPackSerNum[i].Text = " ";	//Can't have a null string
						}

						SaveTestData(txtPoNumbr.Text, txtLotNum.Text, txtCellCode.Text,
                            strPackSerNum[i], txtPackSerNum[i].Text, lblStartDateTime.Text,
							lblStopDateTime.Text, lblResults[i].Text, lblPreTestVtg[i].Text,
							lblLoadVtg[i].Text, lblNoLoadVtg[i].Text, lblIDRes[i].Text,
							strMechanicalFit[i], txtTestTitle.Text);		// Save the test data - rwb 5/27/2015

                        lblTestStatus.Text = "Test Completed";    //rwb 5/27/2015

					}
					break;

				default:
					break;
			}	// End of switch (counter)

			if (counter < 50)
			{
				lblStopDateTime.Text = DateTime.Now.ToString();
				ScanChannels1(0,3,true,4);
				ScanChannels2(0,3,true,4);
			}

			if (counter == -2)	// See if we are doing the ID pin test
			{
				if(chkIDRes.Checked)
				{
					Scan3Avg(IDTestVtgOc1);
					SetRelays(PortNumB, 0xff);	// Turn on the  ID Pin Relays
				}
			}

		}	// End of timer1_Tick(object sender, System.EventArgs e)

		/// <summary>
		/// This method clears the various data fields.
		/// </summary>
		private void ClearLabels(bool ClearSerialNum)
		{
			int i;
			MaxChan = 8;
	
			for (i=0; i < MaxChan; ++i)
			{
				// Clear the label fields

				if (ClearSerialNum)			// Clear the serial number and test flag if true
				{
					lblSerialNum[i].Text = " ";
					txtPackSerNum[i].Text = "";
					TestChannel[i] = false;
				}

				lblPreTestVtg[i].Text = " ";
				lblLoadVtg[i].Text = " ";
				lblNoLoadVtg[i].Text = " ";
				lblIDRes[i].Text = " ";
				lblResults[i].Text = " ";
				strMechanicalFit[i] = " ";

				// Clear the data fields

				PreTestVtg[i] = 0;
				LoadVtg[i] = 0;
				NoLoadVtg[i] = 0;
				IDRes[i] = 0;
			}

			lblElapsedTime.Text = " ";
			lblStartDateTime.Text = " ";
			lblStopDateTime.Text = " ";

		}	// End of ClearLabels(bool ClearSerialNum

		/// <summary>
		/// This method is used to clean up and exit.
		/// </summary>
		private void btnQuit_Click(object sender, System.EventArgs e)
		{
			MccDaq.ErrorInfo ULStat;

			ULStat = MccDaq.MccService.WinBufFree(MemHandle1);	// Release memory buffers
			MemHandle1 = 0;

			ULStat = MccDaq.MccService.WinBufFree(MemHandle2);	// Release memory buffers
			MemHandle2 = 0;

			ClearAllRelays();

			Application.Exit();

		}	// End of btnQuit_Click(object sender, System.EventArgs e)

		/// <summary>
		/// This method is used to select the output file.
		/// </summary>
		private void btnSelectFile_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			openFileDialog1.Title = "Select an Access Database file";
			openFileDialog1.InitialDirectory = "c:\\Aved-Zoll" ;
			openFileDialog1.Filter = "Access files (*.mdb)|*.mdb|All files (*.*)|*.*" ;
			openFileDialog1.RestoreDirectory = false ;

			if(openFileDialog1.ShowDialog() == DialogResult.OK)
				lblFileName.Text = openFileDialog1.FileName;

		}	// End of btnSelectFile_Click(object sender, System.EventArgs e)

		/// <summary>
		/// This method writes the test data to an Access file.
        /// Added TestName string - rwb 5/27/2015
		/// </summary>
		private void SaveTestData(string PoNumber, string LotNumber, string CellDateCode,
			string SerialNumber, string BoardSerialNumber, string StartTime, 
			string FinishTime, string TestResult, string PreTestVoltage, string LoadVoltage,
			string PostTestVoltage, string IDRes, string MechanicalFit, string TestName)
		{
			int ReturnValue = 0;

			OleDbConnection myConnection = null;

			string myConnectionString =
				"Provider=Microsoft.Jet.OLEDB.4.0;" +
				"User Id=;Password=;" +
				@"Data Source=" + lblFileName.Text;

			string myAddQuery = @"INSERT INTO [TestData] " +
				@"(PoNumber, LotNumber, CellDateCode, SerialNumber, BoardSerialNumber, StartTime, FinishTime, TestResult, PreTestVoltage, LoadVoltage, PostTestVoltage, IDRes, MechanicalFit, TestName) " +
				"VALUES ( \"" + PoNumber + "\",\"" +
				LotNumber + "\",\"" +
				CellDateCode + "\",\"" +
				SerialNumber + "\",\"" +
				BoardSerialNumber + "\",\"" +
				StartTime + "\",\"" +
				FinishTime + "\",\"" +
				TestResult + "\",\"" +
				PreTestVoltage + "\",\"" +
				LoadVoltage + "\",\"" +
				PostTestVoltage + "\",\"" +
                IDRes + "\",\"" +
                MechanicalFit + "\",\"" +
                TestName + "\")";

			try
			{
				myConnection = new OleDbConnection(myConnectionString);

				OleDbCommand myCommand =
					new OleDbCommand(myAddQuery, myConnection);

				myConnection.Open();

				ReturnValue = myCommand.ExecuteNonQuery();
			}

			catch (OleDbException e)
			{
				MessageBox.Show("OleDb Error while saving data.", e.Message,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			finally
			{
				if (myConnection != null)
					myConnection.Close();
			}
		}	// End of SaveTestData(......)

		/// <summary>
		/// This method is used to set/clear the relay board
		/// </summary>
		private void SetRelays(MccDaq.DigitalPortType PortNum, ushort DataValue)
		{
			MccDaq.ErrorInfo ULStat1;
			MccDaq.ErrorInfo ULStat2;

			// The relay board is connected to the miniLAB with serial Number 1
			if(Board1SerialNum == 1)
			{
				ULStat1 = DaqBoard1.DOut(PortNum, DataValue);
			}

			if(Board2SerialNum == 1)
			{
				ULStat2 = DaqBoard2.DOut(PortNum, DataValue);
			}
		}	// End of SetRelays(MccDaq.DigitalPortType PortNum, ushort DataValue)

		/// <summary>
		/// This method is used to set/clear the relay board
		/// </summary>
		private void ClearAllRelays()
		{
			MccDaq.ErrorInfo ULStat1;
			MccDaq.ErrorInfo ULStat2;
			ushort DataValue = 0;

			// The relay board is connected to the miniLAB with serial Number 1
			if(Board1SerialNum == 1)
			{
				ULStat1 = DaqBoard1.DOut(PortNumA, DataValue);
				ULStat1 = DaqBoard1.DOut(PortNumB, DataValue);
				ULStat1 = DaqBoard1.DOut(PortNumCL, DataValue);
				ULStat1 = DaqBoard1.DOut(PortNumCH, DataValue);
			}

			if(Board2SerialNum == 1)
			{
				ULStat2 = DaqBoard2.DOut(PortNumA, DataValue);
				ULStat2 = DaqBoard2.DOut(PortNumB, DataValue);
				ULStat2 = DaqBoard2.DOut(PortNumCL, DataValue);
				ULStat2 = DaqBoard2.DOut(PortNumCH, DataValue);
			}
		}	// End of ClearAllRelays()

		/// <summary>
		/// This method is used to test the value of the ID resistor
		/// </summary>
		private void DoIDTest()
		{
			int i;
			float fVdiff;
			float fCurrent;
			double dIDRes, j;
			ushort Bit = 1;
			MaxChan = 8;
			fudge[0] = .095F;
			fudge[1] = .095F;
			fudge[2] = .095F;
			fudge[3] = .095F;
			fudge[4] = .095F;
			fudge[5] = .095F;
			fudge[6] = .1F;
			fudge[7] = .1F;

			// If the chkIDRes box is checked, do the test. Otherwise set it not tested
			if(chkIDRes.Checked)
			{
				TestMask = 0;	//Clear the test mask and update it in this loop

				// We closed the relays back at time = -2 so all should be settled
				Scan3Avg(IDTestVtgID);

				SetRelays(PortNumB, 0);	// Turn off ID Pin Relays

				Scan3Avg(IDTestVtgOc2);	//Grab some more readings

				j = 0;
				for (i=0; i < MaxChan; ++i)
				{
					if (TestChannel[i])	// See if we are testing this guy
					{
////						//Code for testing ID Calculations
//						j = j + .005;
//						IDTestVtgOc1[i,3] = 10.0f + (float) j ;
//						IDTestVtgOc2[i,3] = 10.0f + (float) j ;
//						IDTestVtgID[i,3] = 5.0f;

						lblIDRes[i].ForeColor = System.Drawing.Color.Red;	// Lets be pessimestic
						TestChannel[i] = false;

						fVdiff = ((IDTestVtgOc1[i,3] + IDTestVtgOc2[i,3]) / 2) - fudge[i] - IDTestVtgID[i,3];

						if (fVdiff < 4.0)
						{
							lblIDRes[i].Text = "Fail";	// We're way out
							continue;
						}

						// Calculate the value of the ID Resistor
						fCurrent = fVdiff / 1003;
						dIDRes = Math.Round(IDTestVtgID[i,3] / fCurrent);

						if (dIDRes > 1015.0)
						{
							lblIDRes[i].Text = dIDRes.ToString();	//Show value in red
							continue;	// Bad if over 1.5%
						}

						if (dIDRes < 985.0)
						{
							lblIDRes[i].Text = dIDRes.ToString();	//Show value in red
							continue;	// Bad if under 1.5%
						}

						// Good enough to test. Show value
						TestChannel[i] = true;
						lblIDRes[i].ForeColor = System.Drawing.Color.Green;		//Set green for pass
						lblIDRes[i].Text = dIDRes.ToString();
						// Set the TestMask
						Bit = 1;
						Bit = (ushort)(Bit << i);
						TestMask = (ushort)(TestMask | Bit);

					}	//End of if (TestChannel[i])

				}	// End of for (i=0; i < MaxChan; ++i)

			}		//End of if(chkIDRes.Checked)
			else	//Mark them "Not Tested"
			{
				for (i=0; i < MaxChan; ++i)
				{
					lblIDRes[i].Text = "Not Tested";
				}
			}

			if (TestMask == 0)
			{
				counter = 49;	// Don't run test if all ID's are bad
			}

			if (chkIDResOnly.Checked)
			{
				TestMask = 0xff;
				SaveIDTestData();
				counter = 49;	// Don't run test ID only is checked
			}

		}	//End of IDTest()

		/// <summary>
		/// This method does 3 scans of all the channels and fills the data buffer with average value
		/// </summary>
		private void Scan3Avg(float[,] V3Avg)
		{
			float[] V1 = new float[8];
			float[] V2 = new float[8];
			float[] V3 = new float[8];
			int i;

			SaveEngUnits = V1;	// Scan the Channels
			ScanChannels1(0,3,false,4);
			ScanChannels2(0,3,false,4);

			SaveEngUnits = V2;	// Scan the Channels
			ScanChannels1(0,3,false,4);
			ScanChannels2(0,3,false,4);

			SaveEngUnits = V3;	// Scan the Channels
			ScanChannels1(0,3,false,4);
			ScanChannels2(0,3,false,4);

			for (i=0; i<8; i++)
			{
				V3Avg[i,0] = V1[i];
				V3Avg[i,1] = V2[i];
				V3Avg[i,2] = V3[i];
				V3Avg[i,3] = (V1[i] + V2[i] + V3[i]) / 3;
			}
	
		}	//End of Scan3Avg(float V3Avg)

		/// <summary>
		/// This method saves all of the ID Test Data
		/// </summary>
		private void SaveIDTestData()
		{
			int i,j;

			for (i=0; i < MaxChan; ++i)	// Run this loop to save all the data
			{
				//						if (!TestChannel[i]) continue;	//Continue if the channel isn't being tested
				if (txtPackSerNum[i].Text == "")
				{
					txtPackSerNum[i].Text = " ";	//Can't have a null string
				}


				for (j=0; j < 4; ++j)	//Dump all the values
				{

					lblPreTestVtg[i].Text = IDTestVtgOc1[i,j].ToString();
					lblLoadVtg[i].Text = IDTestVtgID[i,j].ToString();
					lblNoLoadVtg[i].Text = IDTestVtgOc2[i,j].ToString();

					SaveTestData(txtPoNumbr.Text, txtLotNum.Text, txtCellCode.Text,
                        strPackSerNum[i], txtPackSerNum[i].Text, lblStartDateTime.Text,
						lblStopDateTime.Text, lblResults[i].Text, lblPreTestVtg[i].Text,
						lblLoadVtg[i].Text, lblNoLoadVtg[i].Text, lblIDRes[i].Text,
						strMechanicalFit[i], txtTestTitle.Text);		// Save the test data - rwb 5/27/2015

				}	//End of for (j=0; j < 4; ++j)	//Dump all the values

			}	//End of for (i=0; i < MaxChan; ++i)	// Run this loop to save all the data

		}	//End of SaveIDTestData()

		/// <summary>
		/// This method enables/disables the ID Pin test only box
		/// </summary>
		private void chkIDRes_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkIDRes.Checked)
			{
				chkIDResOnly.Enabled = true;
			}
			else
			{
				chkIDResOnly.Enabled = false;
				chkIDResOnly.Checked = false;
			}
		}

        private void Zoll_2015_Load(object sender, EventArgs e)
        {

        }

        private void txtStartSerNum_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPackSerNum2_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblTestStatus_Click(object sender, EventArgs e)
        {

        }

	}	//End of public class Test2 : System.Windows.Forms.Form

}	//End of namespace Zoll_2015

