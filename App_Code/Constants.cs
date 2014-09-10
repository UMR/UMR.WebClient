using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for Constants
/// </summary>
public class Constants
{
	private Constants()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public const string UMR_NURSE_SOAP_CODE_TYPE = "MDS";
    public const string UMR_NURSE_SOAP_MEDCODE = "UMR99441";
    public const int UMR_NURSE_SOAP_CODE_VERSION = 2012;

    public const string UMR_DOCTOR_SOAP_CODE_TYPE = "MDS";
    public const string UMR_DOCTOR_SOAP_MEDCODE = "UMR99442";
    public const int UMR_DOCTOR_SOAP_CODE_VERSION = 2012;

    public const char UMR_SOAP_HOSPITALIZATION = 'F';


    public const int UMR_SOAP_VISIBILITY = 10;
}