using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DataAccess
/// </summary>
public class DataAccess
{
    //Error Messages : User Config
    private const string INSERT_FAILED = "Insert Operation Failed! You do not have required permission to create new users.";
    private const string USER_EXISTS = "Insert Operation Failed! User alredy exists. Please use a different User Id.";
    private const string USER_EXISTS_DB = "Insert Operation Failed! User already exists in the Database. Please use a different User Id.";
    private const string UPDATE_FAILED_NO_PERMISSION = "Update Operation Failed! You do not have required permission to edit user records.";
    private const string DELETE_FAILED_NO_PERMISSION = "Delete Operation Failed! You do not have required permission to delete user records.";
    private const string DELETE_FAILED_USER_CONNECTED = "Delete Operation Failed! Cannot delete a user who is connected";
    private const string PASSWORD_FAILED = "Password Change Operation Failed! Cannot find the specified user.";

    //Sql Server connection object
    private SqlConnection con;
    private string CurrentUserId = string.Empty;

    public DataAccess()
    {
        CurrentUserId = HttpContext.Current == null ? "test" : HttpContext.Current.User.Identity.Name;
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["SQlServerConnectionString"].ConnectionString);
    }

    public bool ValidateUser(string UserName, string Password, out string ErrorMessage)
    {
        string spName = "SP_USERVALIDATE";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmUserId = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        SqlParameter prmPassword = new SqlParameter("p_Password", SqlDbType.NVarChar);
        SqlParameter prmValidate = new SqlParameter("p_Validate", SqlDbType.Int);
        prmValidate.Direction = ParameterDirection.Output;
        //Set values in the parameters
        prmUserId.Value = UserName;
        prmPassword.Value = Password;

        //Add the parameters in...
        cmd.Parameters.Add(prmUserId);
        cmd.Parameters.Add(prmPassword);
        cmd.Parameters.Add(prmValidate);

        ErrorMessage = "";
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            if ((int)prmValidate.Value == 1)
            {
                return true;
            }
            else
            {
                ErrorMessage = "Invalid user name or password";
                return false;
            }
        }
        catch (Exception ex) { LogError(ex); ErrorMessage = ex.Message; return false; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }

        return true;
    }

    //Returns the patient List for the ucSearchGrid ODS...
    public DataSet GetPatientList(string ID, string ModifierID, string FName, string LName, string DateOfBirth)//, int startRowIndex, int maximumRows)
    {
        string spName = "sp_PatientGet";

        //Create the SQLCommand Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmID = new SqlParameter("p_PatientID", SqlDbType.NVarChar);
        SqlParameter prmModifier = new SqlParameter("p_PatientIDModifier", SqlDbType.NVarChar);
        SqlParameter prmFName = new SqlParameter("p_FirstName", SqlDbType.NVarChar);
        SqlParameter prmLName = new SqlParameter("p_LastName", SqlDbType.NVarChar);
        SqlParameter prmDOB = new SqlParameter("p_DOB", SqlDbType.DateTime);

        //Set values in the parameters
        prmID.Value = ID;
        prmModifier.Value = ModifierID;
        prmFName.Value = FName;
        prmLName.Value = LName;

        if (DateOfBirth != null)
        {
            prmDOB.Value = DateTime.Parse(DateOfBirth);
        }


        //Add the parameters in...
        cmd.Parameters.Add(prmID); cmd.Parameters.Add(prmModifier); cmd.Parameters.Add(prmFName);
        cmd.Parameters.Add(prmLName); cmd.Parameters.Add(prmDOB);

        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            cmd.Connection.Open();
            da.Fill(ds);
        }
        catch (Exception ex) { LogError(ex); }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return ds;
    }
    public DataSet GetPatientList(string ID, string ModifierID, string FName, string LName, DateTime DateOfBirth, int currentPageIndex, int pageSize, string orderBy)
    {
        string spName = "SP_PatientGetForPaging";

        //Create the SQLCommand Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        //P_CurrentPage INT, @P_PageSize INT,
        SqlParameter prmStartIndex = new SqlParameter("P_CurrentPage", SqlDbType.Int);
        SqlParameter prmPageSize = new SqlParameter("P_PageSize", SqlDbType.Int);
        SqlParameter prmOrderBy = new SqlParameter("p_OrderBy", SqlDbType.NVarChar);

        SqlParameter prmID = new SqlParameter("p_PatientID", SqlDbType.NVarChar);
        SqlParameter prmModifier = new SqlParameter("p_PatientIDModifier", SqlDbType.NVarChar);
        SqlParameter prmFName = new SqlParameter("p_FirstName", SqlDbType.NVarChar);
        SqlParameter prmLName = new SqlParameter("p_LastName", SqlDbType.NVarChar);
        SqlParameter prmDOB = new SqlParameter("p_DOB", SqlDbType.DateTime);

        //Set values in the parameters
        prmStartIndex.Value = currentPageIndex;
        prmPageSize.Value = pageSize;
        prmOrderBy.Value = orderBy.Trim().Length == 0 ? null : orderBy;

        prmID.Value = ID.Trim().Length == 0 ? null : ID;
        prmModifier.Value = ModifierID.Trim().Length == 0 ? null : ModifierID;
        prmFName.Value = FName.Trim().Length == 0 ? null : FName;
        prmLName.Value = LName.Trim().Length == 0 ? null : LName;

        if (DateOfBirth != null && DateOfBirth != DateTime.MinValue)
        {
            prmDOB.Value = DateOfBirth;
        }


        //Add the parameters in...
        cmd.Parameters.Add(prmStartIndex);
        cmd.Parameters.Add(prmPageSize);
        cmd.Parameters.Add(prmOrderBy);

        cmd.Parameters.Add(prmID);
        cmd.Parameters.Add(prmModifier);
        cmd.Parameters.Add(prmFName);
        cmd.Parameters.Add(prmLName);
        cmd.Parameters.Add(prmDOB);

        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            cmd.Connection.Open();
            da.Fill(ds);
        }
        catch (Exception ex) { LogError(ex); }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return ds;
    }
    public int GetPatientCount(string ID, string ModifierID, string FName, string LName, DateTime DateOfBirth)
    {
        string spName = "SP_PatientGetForPagingCount";

        //Create the SQLCommand Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmID = new SqlParameter("p_PatientID", SqlDbType.NVarChar);
        SqlParameter prmModifier = new SqlParameter("p_PatientIDModifier", SqlDbType.NVarChar);
        SqlParameter prmFName = new SqlParameter("p_FirstName", SqlDbType.NVarChar);
        SqlParameter prmLName = new SqlParameter("p_LastName", SqlDbType.NVarChar);
        SqlParameter prmDOB = new SqlParameter("p_DOB", SqlDbType.DateTime);

        //Set values in the parameters
        prmID.Value = ID.Trim().Length == 0 ? null : ID;
        prmModifier.Value = ModifierID.Trim().Length == 0 ? null : ModifierID;
        prmFName.Value = FName.Trim().Length == 0 ? null : FName;
        prmLName.Value = LName.Trim().Length == 0 ? null : LName;

        if (DateOfBirth != null && DateOfBirth != DateTime.MinValue)
        {
            prmDOB.Value = DateOfBirth;
        }


        //Add the parameters in...
        cmd.Parameters.Add(prmID); cmd.Parameters.Add(prmModifier); cmd.Parameters.Add(prmFName);
        cmd.Parameters.Add(prmLName); cmd.Parameters.Add(prmDOB);

        int count = 0;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            cmd.Connection.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
        }
        catch (Exception ex) { LogError(ex); }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return count;
    }
    //Returns the Demogrphic info to the ucDemographics
    public DataSet GetPatientDemographics(long patientKey, string UserId)
    {
        string spName = "sp_PatientGetByPK";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmUserId = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        prmUserId.Value = UserId;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmUserId);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    //Return the saved ds for HealthCareInfo
    public DataSet GetPrincipalHealthCareInfo(long patientKey, string UserId)
    {
        string spName = "sp_PrincipalHealthCareByPK";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmUserId = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        prmUserId.Value = UserId;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmUserId);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetRemarkableDisciplineList(long patientKey, string User)
    {
        return GetRemarkableDisciplineList(patientKey, User, null, null);
    }
    //Return the list of RD 
    public DataSet GetRemarkableDisciplineList(long patientKey, string User, DateTime? fromDate, DateTime? toDate)
    {
        string spName = "sp_RemarkableSummaryGetByPK";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmUserId = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        SqlParameter prmFromDate = new SqlParameter("p_FromDate", SqlDbType.DateTime);
        SqlParameter prmToDate = new SqlParameter("p_ToDate", SqlDbType.DateTime);
        SqlParameter prmAllowInsert = new SqlParameter("p_AllowInsert", SqlDbType.Int);

        if (string.IsNullOrEmpty(User))
        {
            User = HttpContext.Current.User.Identity.Name;
            prmAllowInsert.Value = 0;
        }
        else
        {
            prmAllowInsert.Value = 1;
        }
        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        prmUserId.Value = User;
        if (fromDate != null)
        {
            prmFromDate.Value = fromDate.Value;
        }
        if (toDate != null)
        {
            prmToDate.Value = toDate.Value;
        }

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmUserId); cmd.Parameters.Add(prmFromDate);
        cmd.Parameters.Add(prmToDate); cmd.Parameters.Add(prmAllowInsert);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataTable GetSortedRDs(string disCodes)
    {
        string query = string.Format(@"SELECT * FROM UMR_DICTIONARY WHERE ID_NUM IN ({0}) AND STEP = 0 ORDER BY Detail", disCodes);

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;

        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }

    public DataSet GetRemarkableDisciplineListForMedCode(string medCode, string excludedDisCode)
    {
        string query = string.Format(@"SELECT DISTINCT D.DETAIL,C.DISCODE , MEDCODE, DISCODE
                                        FROM UMR_CLASSIFIER C
                                        INNER JOIN UMR_MEDCODES MC ON C.GUID=MC.GUID
                                        INNER JOIN UMR_DICTIONARY D ON C.DISCODE = D.ID_NUM
                                        WHERE MC.MEDCODE='{0}' and C.DISCODE <> '{1}' AND D.STEP=0", medCode, excludedDisCode);

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;

        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }

    }

    public DataSet GetDistinctSpecificServiceDate(long patientKey, string codeType)
    {
        string PATIENTKEY = "@patientKey";
        SqlParameter patientParam = new SqlParameter(PATIENTKEY,patientKey);

        string CODETYPE = "@codeType";
        SqlParameter codeTypeParam = new SqlParameter(CODETYPE,codeType);

        string query = string.Format(@"SELECT DISTINCT(CONVERT(VARCHAR(20), dbo.[UMR_PATIENT_DIAGNOSES].CODEDATE, 110))
                                        FROM dbo.[UMR_PATIENT_DIAGNOSES]
                                        WHERE dbo.UMR_PATIENT_DIAGNOSES.CODE_TYPE = {0}
                                        AND dbo.UMR_PATIENT_DIAGNOSES.PATIENT_KEY = {1}", CODETYPE, PATIENTKEY);

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddRange(new SqlParameter[] { codeTypeParam, patientParam });

        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }

    }

    public DataSet GetSpecificServiceDetails(long patientKey, string codeType, DateTime serviceDate)
    {
        string PATIENTKEY = "@patientKey";
        SqlParameter patientParam = new SqlParameter(PATIENTKEY, patientKey);

        string CODETYPE = "@codeType";
        SqlParameter codeTypeParam = new SqlParameter(CODETYPE, codeType);

        string SVCDAY = "@serviceDay";
        SqlParameter serviceDayParam = new SqlParameter(SVCDAY, serviceDate.Day);
        string SVCMONTH = "@serviceMonth";
        SqlParameter serviceMonthParam = new SqlParameter(SVCMONTH, serviceDate.Month);
        string SVCYEAR = "@serviceYear";
        SqlParameter serviceYearParam = new SqlParameter(SVCYEAR, serviceDate.Year);

        string query = string.Format(@"SELECT 
                                        dbo.[UMR_MEDCODES].MEDCODE,
                                        dbo.[UMR_MEDCODES].CODE_TYPE,
                                        dbo.[UMR_MEDCODES].MEDICAL_CONTENT_INDEX, 
                                        dbo.[UMR_MEDCODES].DETAIL, 
                                        CONVERT(VARCHAR(20), dbo.[UMR_PATIENT_DIAGNOSES].CODEDATE, 110) AS CODEDATE,
                                        dbo.UMR_MEDCODES.[MEDCODE_VALUE] AS VALUE
                                        FROM dbo.[UMR_MEDCODES], dbo.[UMR_PATIENT_DIAGNOSES]
                                        WHERE dbo.UMR_MEDCODES.CODE_TYPE = dbo.UMR_PATIENT_DIAGNOSES.CODE_TYPE 
                                        AND dbo.UMR_MEDCODES.MEDCODE = dbo.UMR_PATIENT_DIAGNOSES.MEDCODE 
                                        AND dbo.UMR_MEDCODES.CODE_VERSION = dbo.UMR_PATIENT_DIAGNOSES.CODE_VERSION 
                                        AND dbo.UMR_PATIENT_DIAGNOSES.CODE_TYPE = {0}
                                        AND dbo.UMR_PATIENT_DIAGNOSES.PATIENT_KEY = {1}
                                        AND DAY(dbo.[UMR_PATIENT_DIAGNOSES].CODEDATE) = {2}
                                        AND MONTH(dbo.[UMR_PATIENT_DIAGNOSES].CODEDATE) = {3}
                                        AND YEAR(dbo.[UMR_PATIENT_DIAGNOSES].CODEDATE) = {4}", CODETYPE, PATIENTKEY, SVCDAY, SVCMONTH, SVCYEAR);

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddRange(new SqlParameter[] { codeTypeParam, patientParam, serviceDayParam, serviceMonthParam, serviceYearParam });

        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }

    }


    public DataSet GetRemarkableDisciplineDetail(string disCode)
    {
        string query = string.Format(@"SELECT DETAIL FROM UMR_DICTIONARY WHERE ID_NUM='{0}' AND STEP=0", disCode);

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;

        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }

    }
    public SqlDataReader GetRDDiagnosisDR(long patientKey, string codeType, string medCode, decimal codeVersion, decimal disCode)
    {
        string spName = "sp_RemarkableDisciplineDIAGet";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmCodeType = new SqlParameter("p_CodeType", SqlDbType.NVarChar);
        SqlParameter prmMedCode = new SqlParameter("p_MedCode", SqlDbType.NVarChar);
        SqlParameter prmCodeVersion = new SqlParameter("p_CodeVersion", SqlDbType.Decimal);
        SqlParameter prmDisCode = new SqlParameter("p_DisCode", SqlDbType.Decimal);

        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        prmCodeType.Value = codeType;
        prmMedCode.Value = medCode;
        prmCodeVersion.Value = codeVersion;
        prmDisCode.Value = disCode;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey); cmd.Parameters.Add(prmCodeType);
        cmd.Parameters.Add(prmMedCode); cmd.Parameters.Add(prmCodeVersion); cmd.Parameters.Add(prmDisCode);

        //Create the DataSet
        DataSet ds = new DataSet();
        SqlDataReader dr;
        try
        {
            cmd.Connection.Open();
            dr = cmd.ExecuteReader();

            return dr;
        }
        catch (Exception ex) { LogError(ex); cmd.Connection.Close(); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetRDDiagnosis(long patientKey, decimal disCode)
    {
        return GetRDDiagnosis(patientKey, disCode, null, null);
    }
    public DataSet GetRDDiagnosis(long patientKey, decimal disCode, DateTime? fromDate, DateTime? toDate)
    {
        string spName = "sp_RemarkableDisciplineDIAGet";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmDisCode = new SqlParameter("p_DisCode", SqlDbType.Int);
        SqlParameter prmFromDate = new SqlParameter("p_FromDate", SqlDbType.DateTime);
        SqlParameter prmToDate = new SqlParameter("p_ToDate", SqlDbType.DateTime);
        SqlParameter prmUserID = new SqlParameter("p_UserID", SqlDbType.NVarChar);

        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        prmDisCode.Value = disCode;
        if (fromDate != null)
        {
            prmFromDate.Value = fromDate.Value;
        }
        if (toDate != null)
        {
            prmToDate.Value = toDate.Value;
        }
        prmUserID.Value = CurrentUserId;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmDisCode);
        cmd.Parameters.Add(prmFromDate);
        cmd.Parameters.Add(prmToDate);
        cmd.Parameters.Add(prmUserID);

        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            int totalRows = ds.Tables[0].Rows.Count;
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetRDDiagnosisDetail(long patientKey, string medCode)
    {
        string spName = "sp_DiagnosisGetByPatient";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmMedCode = new SqlParameter("p_MedCode", SqlDbType.NVarChar);
        SqlParameter prmUserId = new SqlParameter("p_UserID", SqlDbType.NVarChar);

        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        prmMedCode.Value = medCode;
        prmUserId.Value = CurrentUserId;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmMedCode);
        cmd.Parameters.Add(prmUserId);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetRDMedication(long patientKey)
    {
        return GetRDMedication(patientKey, null, null);
    }
    public DataSet GetRDMedication(long patientKey, DateTime? fromDate, DateTime? toDate)
    {
        string spName = "sp_RemarkableDisciplineNDCGet";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmFromDate = new SqlParameter("p_FromDate", SqlDbType.DateTime);
        SqlParameter prmToDate = new SqlParameter("p_ToDate", SqlDbType.DateTime);
        SqlParameter prmUserId = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        if (fromDate != null)
        {
            prmFromDate.Value = fromDate.Value;
        }
        if (toDate != null)
        {
            prmToDate.Value = toDate.Value;
        }
        prmUserId.Value = CurrentUserId;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmFromDate);
        cmd.Parameters.Add(prmToDate);
        cmd.Parameters.Add(prmUserId);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetRDMedicationDetail(long patientKey, string ndcCode)
    {
        string spName = "sp_NDCGetByPatient";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmNDCCode = new SqlParameter("p_NDCCode", SqlDbType.NVarChar);

        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        prmNDCCode.Value = ndcCode;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmNDCCode);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetProviderInfo(string id)
    {
        string spName = "sp_DoctorGetByPK";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmID = new SqlParameter("p_DoctorID", SqlDbType.NVarChar);
        //Set values in the parameters
        prmID.Value = id;
        //Add the parameters in...
        cmd.Parameters.Add(prmID);

        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetMPIInfo(long patientKey)
    {
        string spName = "sp_MasterPatientIndexGet";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmUserID = new SqlParameter("UserID", SqlDbType.NVarChar);

        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        prmUserID.Value = CurrentUserId;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmUserID);
        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetMPIInfoSpecific(long patientKey, string doctorID, string serviceDate)
    {
        string spName = "sp_MasterPatientGetSpecific";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmDoctorID = new SqlParameter("p_DoctorID", SqlDbType.NVarChar);
        SqlParameter prmServiceDate = new SqlParameter("p_ServiceDate", SqlDbType.DateTime);
        SqlParameter prmUserID = new SqlParameter("p_UserID", SqlDbType.NVarChar);

        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        prmDoctorID.Value = doctorID;
        prmServiceDate.Value = serviceDate;
        prmUserID.Value = CurrentUserId;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmDoctorID);
        cmd.Parameters.Add(prmServiceDate);
        cmd.Parameters.Add(prmUserID);
        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetLRPMInfo(long patientKey)
    {
        string spName = "sp_LastPrescriptionMedication";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmUserID = new SqlParameter("p_UserID", SqlDbType.NVarChar);

        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        prmUserID.Value = CurrentUserId;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmUserID);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetLRAInfo(object patientKey)
    {
        string spName = "sp_LastAccessedGet";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);

        //Set values in the parameters
        prmPatientKey.Value = patientKey;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetMultipleProviderInfo(long patientKey)
    {
        return GetMultipleProviderInfo(patientKey, null, null);
    }
    public DataSet GetMultipleProviderInfo(long patientKey, DateTime? fromDate, DateTime? toDate)
    {
        string spName = "sp_DoctorGetByPatient";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmFromDate = new SqlParameter("p_FromDate", SqlDbType.DateTime);
        SqlParameter prmToDate = new SqlParameter("p_ToDate", SqlDbType.DateTime);

        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        if (fromDate != null)
        {
            prmFromDate.Value = fromDate.Value;
        }
        if (toDate != null)
        {
            prmToDate.Value = toDate.Value;
        }

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmFromDate);
        cmd.Parameters.Add(prmToDate);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetLastDates(long patientKey)
    {
        string spName = "sp_PatientLastDateGetByPK";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);

        //Set values in the parameters
        prmPatientKey.Value = patientKey;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public List<string> GetAllRD()
    {
        string spName = "sp_DictionaryLevel1Get";

        //Create the List to return
        List<string> pList = new List<string>();

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the DataReader
        SqlDataReader dr;
        try
        {
            cmd.Connection.Open();
            dr = cmd.ExecuteReader();
            using (dr)
            {
                while (dr.Read())
                {
                    //Add to the list
                    pList.Add(dr[0].ToString());
                }
            }
        }
        catch (Exception ex) { LogError(ex); }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return pList;
    }
    public DataTable GetUserSettings(string UserID) //SP Not Found
    {
        string spName = "sp_UserSettingsGet";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmID = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        //Set values in the parameters
        prmID.Value = UserID;

        //Add the parameters in...
        cmd.Parameters.Add(prmID);

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public void SaveUserSettings(string UserID, string UserSettings)//SP Not Found
    {
        string spName = "sp_UserSettingsUpdate";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmID = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        //SqlParameter prmSettings = new SqlParameter("p_UserSettings", SqlDbType.XmlType);
        SqlParameter prmSettings = new SqlParameter("p_UserSettings", SqlDbType.Text);

        //Set values in the parameters
        prmID.Value = UserID;
        prmSettings.Value = UserSettings;

        //Add the parameters in...
        cmd.Parameters.Add(prmID); cmd.Parameters.Add(prmSettings);

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (SqlException Ex) { LogError(Ex); throw; }
        catch (Exception ex) { LogError(ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataTable GetAllPatientModifiers()
    {
        string spName = "sp_PatientModifiersGet";

        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataTable GetInsuranceInformation(long patientKey)
    {
        string spName = "sp_PatientInsuranceGetByPK";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);

        //Set values in the parameters
        prmPatientKey.Value = patientKey;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataTable GetIdForLastPatientAccessed(string UserId)
    {
        string spName = "sp_UserLastPatientAccess";
        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmUser = new SqlParameter("p_UserID", SqlDbType.NVarChar);

        //Set values in the parameters
        prmUser.Value = UserId;

        //Add the parameters in...
        cmd.Parameters.Add(prmUser);

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataTable GetNRDList(long patientKey)
    {
        string spName = "sp_UnRemarkableDispGet";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmUserId = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);

        //Set values in the parameters
        prmUserId.Value = CurrentUserId;
        prmPatientKey.Value = patientKey;


        //Add the parameters in...
        cmd.Parameters.Add(prmUserId);
        cmd.Parameters.Add(prmPatientKey);

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataTable GetAMDForPatient(long patientKey)
    {
        string spName = "sp_PatientAMDGet";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);


        //Set values in the parameters
        prmPatientKey.Value = patientKey;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    // This method will run for each RD on the Result Page + RD PopUp
    // We r going to DB to fetch the date only once, the rest requests will be served from session
    // We r creating the Session Key to make it unique per patient because : (Different Patient == Differnt MaxOfCodeDate)
    public DateTime GetMaxOfCodeDate(long patientKey, char Identifier)
    {
        string MaxOfDIADateKey = patientKey + "MaxOfCodeDateDiagnosis";
        string MaxOfNDCDateKey = patientKey + "MaxOfCodeDateNDC";
        switch (Identifier)
        {
            case 'D':
                object objMaxDIADate = HttpContext.Current.Session[MaxOfDIADateKey];
                if (objMaxDIADate != null)
                {
                    return (DateTime)objMaxDIADate;
                }
                break;

            case 'N':
                object objMaxNDCDate = HttpContext.Current.Session[MaxOfNDCDateKey];
                if (objMaxNDCDate != null)
                {
                    return (DateTime)objMaxNDCDate;
                }
                break;
        }
        // If we have made it this far, that means we didn't find the item in the Session, so Fetch...
        string spName = "sp_MaxofMaxNDCDIADateGet";

        //Create the Oracle Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmUserID = new SqlParameter("p_UserID", SqlDbType.NVarChar);

        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        prmUserID.Value = CurrentUserId;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmUserID);

        SqlDataReader dr;
        DateTime MaxDiaDate = new DateTime();
        DateTime MaxNDCDate = new DateTime();

        cmd.Connection.Open();
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (dr.Read())
        {
            if (dr["MaxofMaxDIADate"] != null && dr["MaxofMaxDIADate"] != DBNull.Value)
            {
                MaxDiaDate = (DateTime)dr["MaxofMaxDIADate"];
            }
            if (dr["MaxofMaxNDCDate"] != null && dr["MaxofMaxNDCDate"] != DBNull.Value)
            {
                MaxNDCDate = (DateTime)dr["MaxofMaxNDCDate"];
            }
            //Now Add to the Session
            HttpContext.Current.Session.Add(MaxOfDIADateKey, MaxDiaDate);
            HttpContext.Current.Session.Add(MaxOfNDCDateKey, MaxNDCDate);
        }
        DateTime returnDate = new DateTime();
        switch (Identifier)
        {
            case 'D':
                returnDate = MaxDiaDate;
                break;
            case 'N':
                returnDate = MaxNDCDate;
                break;
        }
        return returnDate;
    }

    #region Configuration Page Methods

    public DataTable GetAllProviders()
    {
        string spName = "sp_ProvidersGetAll";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataTable GetDisciplines()
    {

        DataTable dtDisc = HttpContext.Current.Cache["DisciplinesLookUp"] as DataTable;
        if (dtDisc != null)
        {
            return dtDisc;
        }

        string strSelect = "Select * from vw_Disciplines";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(strSelect, con);
        cmd.CommandType = CommandType.Text;

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Close();
            da.Fill(dt);
            HttpContext.Current.Cache.Insert("DisciplinesLookUp", dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public int UpdateProvider(Hashtable Values, string ProviderID)
    {
        //*****************************************************************************************
        string spName = "sp_ProviderEdit";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmID = new SqlParameter("p_ProviderID", SqlDbType.NVarChar);
        SqlParameter prmLName = new SqlParameter("p_LastName", SqlDbType.NVarChar);
        SqlParameter prmFName = new SqlParameter("p_FirstName", SqlDbType.NVarChar);
        SqlParameter prmPhone = new SqlParameter("p_Phone", SqlDbType.NVarChar);
        SqlParameter prmFax = new SqlParameter("p_Fax", SqlDbType.NVarChar);
        SqlParameter prmCell = new SqlParameter("p_CellPhone", SqlDbType.NVarChar);
        SqlParameter prmPager = new SqlParameter("p_Pager", SqlDbType.NVarChar);
        SqlParameter prmInsID = new SqlParameter("p_InstitutionID", SqlDbType.NVarChar);
        SqlParameter prmDisc = new SqlParameter("p_Discipline", SqlDbType.Int);
        SqlParameter prmWeb = new SqlParameter("p_WebSite", SqlDbType.NVarChar);
        SqlParameter prmEmail = new SqlParameter("p_Email", SqlDbType.NVarChar);
        //Set values in the parameters
        prmID.Value = ProviderID;
        prmLName.Value = Values["LastName"];
        prmFName.Value = Values["FirstName"]; prmPhone.Value = Values["Phone"];
        prmFax.Value = Values["Fax"]; prmCell.Value = Values["CellPhone"];
        prmPager.Value = Values["Pager"]; prmInsID.Value = Values["InstitutionID"];
        prmDisc.Value = Values["Discipline"]; prmWeb.Value = Values["WebSite"]; prmEmail.Value = Values["Email"];

        //Add the parameters in...
        cmd.Parameters.Add(prmID);
        cmd.Parameters.Add(prmLName);
        cmd.Parameters.Add(prmFName); cmd.Parameters.Add(prmPhone); cmd.Parameters.Add(prmFax); cmd.Parameters.Add(prmCell);
        cmd.Parameters.Add(prmPager); cmd.Parameters.Add(prmInsID); cmd.Parameters.Add(prmDisc); cmd.Parameters.Add(prmWeb); cmd.Parameters.Add(prmEmail);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (Exception ex) { LogError(ex); return -1; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return 1;
        //*****************************************************************************************
    }
    public int InsertProvider(Hashtable Values)
    {
        string spName = "sp_ProviderAdd";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmID = new SqlParameter("p_ProviderID", SqlDbType.NVarChar);
        SqlParameter prmLName = new SqlParameter("p_LastName", SqlDbType.NVarChar);
        SqlParameter prmFName = new SqlParameter("p_FirstName", SqlDbType.NVarChar);
        SqlParameter prmPhone = new SqlParameter("p_Phone", SqlDbType.NVarChar);
        SqlParameter prmFax = new SqlParameter("p_Fax", SqlDbType.NVarChar);
        SqlParameter prmCell = new SqlParameter("p_CellPhone", SqlDbType.NVarChar);
        SqlParameter prmPager = new SqlParameter("p_Pager", SqlDbType.NVarChar);
        SqlParameter prmInsID = new SqlParameter("p_InstitutionID", SqlDbType.NVarChar);
        SqlParameter prmDisc = new SqlParameter("p_Discipline", SqlDbType.Int);
        SqlParameter prmWeb = new SqlParameter("p_WebSite", SqlDbType.NVarChar);
        SqlParameter prmEmail = new SqlParameter("p_Email", SqlDbType.NVarChar);
        //Set values in the parameters
        prmID.Value = Values["ProviderID"];
        prmLName.Value = Values["LastName"]; prmFName.Value = Values["FirstName"];
        prmPhone.Value = Values["Phone"]; prmFax.Value = Values["Fax"];
        prmCell.Value = Values["CellPhone"]; prmPager.Value = Values["Pager"];
        prmInsID.Value = Values["InstitutionID"]; prmDisc.Value = Values["Discipline"];
        prmWeb.Value = Values["WebSite"]; prmEmail.Value = Values["Email"];

        //Add the parameters in...
        cmd.Parameters.Add(prmID);
        cmd.Parameters.Add(prmLName); cmd.Parameters.Add(prmFName); cmd.Parameters.Add(prmPhone);
        cmd.Parameters.Add(prmFax); cmd.Parameters.Add(prmCell); cmd.Parameters.Add(prmPager);
        cmd.Parameters.Add(prmInsID); cmd.Parameters.Add(prmDisc); cmd.Parameters.Add(prmWeb);
        cmd.Parameters.Add(prmEmail);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (Exception ex) { LogError(ex); throw ex; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return 1;
    }
    public int DeleteProvider(string ProviderID)
    {
        string spName = "sp_ProviderDelete";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmID = new SqlParameter("p_ProviderID", SqlDbType.NVarChar);
        //Set values in the parameters
        prmID.Value = ProviderID;
        //Add the parameters in...
        cmd.Parameters.Add(prmID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (Exception ex) { LogError(ex); return -1; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return 1;
    }
    public object GetAllUsers()
    {
        string spName = "sp_UsersGetAll";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public int UpdateUser(Hashtable Values, string userID, out string ErrorMessage)
    {
        DataTable dtUserInfo = GetUserInfo(CurrentUserId);
        if (dtUserInfo != null && dtUserInfo.Rows.Count > 0)
        {
            int group = Int32.Parse(dtUserInfo.Rows[0]["USER_GROUP"].ToString());
            if (group < 10)
            {
                ErrorMessage = UPDATE_FAILED_NO_PERMISSION;
                return -1;
            }
        }
        string spName = "sp_UserEdit";
        ErrorMessage = "";
        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmID = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        SqlParameter prmFName = new SqlParameter("p_FirstName", SqlDbType.NVarChar);
        SqlParameter prmLName = new SqlParameter("p_LastName", SqlDbType.NVarChar);
        SqlParameter prmVisibility = new SqlParameter("p_Visibility", SqlDbType.Int); //***
        SqlParameter prmGroup = new SqlParameter("p_UserGroup", SqlDbType.Int);//****
        SqlParameter prmEmail = new SqlParameter("p_Email", SqlDbType.NVarChar);
        SqlParameter prmPhone = new SqlParameter("p_Phone", SqlDbType.NVarChar);
        SqlParameter prmIndustry = new SqlParameter("p_Industry", SqlDbType.NVarChar);
        SqlParameter prmFax = new SqlParameter("p_Fax", SqlDbType.NVarChar);
        SqlParameter prmActive = new SqlParameter("p_Active", SqlDbType.Int);

        //Set values in the parameters
        prmID.Value = userID;
        prmLName.Value = Values["LastName"].ToString(); prmFName.Value = Values["FirstName"].ToString();
        prmPhone.Value = Values["Phone"].ToString(); prmEmail.Value = Values["Email"].ToString();
        Decimal decVisibility;
        if (Decimal.TryParse(Values["Visibility"].ToString(), out decVisibility))
        {
            prmVisibility.Value = decVisibility;
        }
        Decimal decGroup;
        if (Decimal.TryParse(Values["UserGroup"].ToString(), out decGroup))
        {
            prmGroup.Value = decGroup;
        }
        prmIndustry.Value = Values["Industry"].ToString();
        prmFax.Value = Values["Fax"].ToString();
        if (Boolean.Parse(Values["Active"].ToString()) == true)
        {
            prmActive.Value = "1";
        }
        else
        {
            prmActive.Value = "0";
        }
        //Add the parameters in...
        cmd.Parameters.Add(prmID);
        cmd.Parameters.Add(prmFName);
        cmd.Parameters.Add(prmLName);
        cmd.Parameters.Add(prmVisibility);
        cmd.Parameters.Add(prmGroup);
        cmd.Parameters.Add(prmEmail);
        cmd.Parameters.Add(prmPhone);
        cmd.Parameters.Add(prmIndustry);
        cmd.Parameters.Add(prmFax);
        cmd.Parameters.Add(prmActive);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (SqlException Ex)
        {
            LogError(Ex);
            switch (Ex.Number)
            {
                case 20011:
                    ErrorMessage = UPDATE_FAILED_NO_PERMISSION;
                    break;
                default:
                    ErrorMessage = "An Unspecified Error Occurred[Error Code : " + Ex.Number.ToString() + "]. Please contact your administrator with the error code to resolve this issue";
                    break;
            }
            return -1;
        }
        catch (Exception ex) { LogError(ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return 1;
    }
    public int InsertUser(Hashtable Values, out string ErrorMessage)
    {

        DataTable dtUserInfo = GetUserInfo(CurrentUserId);
        if (dtUserInfo != null && dtUserInfo.Rows.Count > 0)
        {
            int group = Int32.Parse(dtUserInfo.Rows[0]["USER_GROUP"].ToString());
            if (group < 10)
            {
                ErrorMessage = INSERT_FAILED;
                return -1;
            }
        }
        string spName = "sp_UserAdd";
        ErrorMessage = "";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmID = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        SqlParameter prmPassword = new SqlParameter("p_password", SqlDbType.NVarChar);
        SqlParameter prmLName = new SqlParameter("p_LastName", SqlDbType.NVarChar);
        SqlParameter prmFName = new SqlParameter("p_FirstName", SqlDbType.NVarChar);
        SqlParameter prmVisibility = new SqlParameter("p_Visibility", SqlDbType.Int); //***
        SqlParameter prmGroup = new SqlParameter("p_UserGroup", SqlDbType.Int);//****
        SqlParameter prmEmail = new SqlParameter("p_Email", SqlDbType.NVarChar);
        SqlParameter prmPhone = new SqlParameter("p_Phone", SqlDbType.NVarChar);
        SqlParameter prmIndustry = new SqlParameter("p_Industry", SqlDbType.NVarChar);
        SqlParameter prmFax = new SqlParameter("p_Fax", SqlDbType.NVarChar);

        //Set values in the parameters
        prmID.Value = Values["UserID"].ToString().Trim();
        prmPassword.Value = Values["Password"].ToString().Trim();
        prmLName.Value = Values["LastName"].ToString();
        prmFName.Value = Values["FirstName"].ToString();
        prmPhone.Value = Values["Phone"].ToString();
        prmEmail.Value = Values["Email"].ToString();
        //prmVisibility.Value = Values["Visibility"]; 
        //prmGroup.Value = Values["Group"];
        prmIndustry.Value = Values["Industry"].ToString();
        prmFax.Value = Values["Fax"].ToString();

        Decimal decVisibility;
        if (Decimal.TryParse(Values["Visibility"].ToString(), out decVisibility))
        {
            prmVisibility.Value = decVisibility;
        }
        Decimal decGroup;
        if (Decimal.TryParse(Values["Group"].ToString(), out decGroup))
        {
            prmGroup.Value = decGroup;
        }

        //Add the parameters in...
        cmd.Parameters.Add(prmID);
        cmd.Parameters.Add(prmPassword);
        cmd.Parameters.Add(prmFName);
        cmd.Parameters.Add(prmLName);
        cmd.Parameters.Add(prmVisibility);
        cmd.Parameters.Add(prmGroup);
        cmd.Parameters.Add(prmEmail);
        cmd.Parameters.Add(prmPhone);
        cmd.Parameters.Add(prmIndustry);
        cmd.Parameters.Add(prmFax);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (SqlException Ex)
        {
            LogError(Ex);
            //switch (Ex.Number)
            //{
            //    case 20010:
            //        ErrorMessage = INSERT_FAILED;
            //        break;
            //    case 20001:
            //        ErrorMessage = USER_EXISTS;
            //        break;
            //    case 20002:
            //        ErrorMessage = USER_EXISTS_DB;
            //        break;
            //    default:
            //        ErrorMessage = "An Unspecified Error Occurred[Error Code : " + Ex.Number.ToString() + "]. Please contact your administrator with the error code to resolve this issue";
            //        break;
            //}
            ErrorMessage = "An Unspecified Error Occurred[Error Code : " + Ex.ToString() + "]. Please contact your administrator with the error code to resolve this issue";

            return -1;
        }
        catch (Exception Ex) { LogError(Ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return 1;
    }
    public int DeleteUser(string userID, out string ErrorMessage)
    {
        string spName = "sp_UserDelete";
        ErrorMessage = "";
        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmID = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        //Set values in the parameters
        prmID.Value = userID;
        //Add the parameters in...
        cmd.Parameters.Add(prmID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (SqlException Ex)
        {
            LogError(Ex);
            //switch (Ex.Number)
            //{
            //    case 20012:
            //        ErrorMessage = DELETE_FAILED_NO_PERMISSION;
            //        break;
            //    case 1940:
            //        ErrorMessage = DELETE_FAILED_USER_CONNECTED;
            //        break;
            //    default:
            //        ErrorMessage = "An Unspecified Error Occurred[Error Code : " + Ex.Number.ToString() + "]. Please contact your administrator with the error code to resolve this issue";
            //        break;
            //}
            ErrorMessage = "An Unspecified Error Occurred[Error Code : " + Ex.ToString() + "]. Please contact your administrator with the error code to resolve this issue";

            return -1;
        }
        catch (Exception ex) { LogError(ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return 1;
    }

    #endregion

    public DataTable GetPatientDemographicGeneralInfo(long patientKey)
    {
        string strSQL = "Select First_Name AS FirstName, Last_Name AS LastName from UMR_Patient WHERE PATIENT_KEY = ";
        strSQL += patientKey;

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.CommandType = CommandType.Text;

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataTable GetUserInfo(string UserName)
    {
        string strSQL = "Select USER_ID as UserID,First_Name AS FirstName, Last_Name AS LastName,EMAIL as Email,PHONE as Phone,FAX as Fax,Industry,Website,USER_GROUP from UMR_USERS WHERE lower(USER_ID) = '" + UserName + "'";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.CommandType = CommandType.Text;

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    private void LogError(Exception ex)
    {
        ExceptionPolicy.HandleException(ex, "Exception Log Policy");
    }
    public DataTable GetProviderCodeDateHistory(string doctorId, long patientKey)
    {
        string strSQL = "Select ND.codedate as CodeDate from( " +
                        " SELECT DISTINCT N.CODEDATE as codedate " +
                        " FROM UMR_Patient_NDC N " +
                        " where N.DOCTOR_ID='" + doctorId + "' and N.PATIENT_KEY = " + patientKey + "" +
                        " union" +
                        " SELECT DISTINCT D.CODEDATE " +
                        " FROm UMR_Patient_Diagnoses D	 " +
                        "where D.DOCTOR_ID='" + doctorId + "' and D.PATIENT_KEY = " + patientKey + "" +
                        " )ND order by CodeDate DESC";


        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.CommandType = CommandType.Text;

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataTable GetInstitutionCodeDateHistory(string institutionID, long patientKey)
    {
        string strSQL = "Select ND.codedate as CodeDate from( " +
                        " SELECT DISTINCT N.CODEDATE as codedate " +
                        " FROM UMR_Patient_NDC N " +
                        " where N.INSTITUTION_CODE='" + institutionID + "' and N.PATIENT_KEY = " + patientKey + " " +
                        " union" +
                        " SELECT DISTINCT D.CODEDATE " +
                        " FROm UMR_Patient_Diagnoses D	 " +
                        "where D.INSTITUTION_CODE='" + institutionID + "' and D.PATIENT_KEY = " + patientKey + " " +
                        " )ND order by CodeDate DESC";


        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.CommandType = CommandType.Text;

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetEmergencyContact(long patientKey, string UserId)
    {
        string spName = "sp_PatientContactGetByPK";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmUserId = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        prmUserId.Value = UserId;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmUserId);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);

            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public string GetMinDate(int discode, long patientKey)
    {
        string strSQL = "";
        if (discode == 0)
        {
            strSQL = "select MIN(ND.CODEDATE) as MinDate  from UMR_PATIENT_NDC ND" +
                   " where ND.PATIENT_KEY = " + patientKey + " ";
            // return "01-JAN-2008";
        }
        else
        {
            strSQL = "select MIN(DG.CODEDATE) as MinDate from UMR_LEVEL1 L1 " +
                     " INNER JOIN UMR_PATIENT_DIAGNOSES DG on L1.MEDCODE=DG.MEDCODE " +
                     " where L1.DISCODE=" + discode + " and DG.PATIENT_KEY =" + patientKey + " ";


        }

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.CommandType = CommandType.Text;

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt.Rows[0]["MinDate"].ToString();
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public string GetMaxDate(int discode, long patientKey)
    {
        string strSQL = "";
        if (discode == 0)
        {
            strSQL = "select MAX(ND.CODEDATE) as MaxDate  from UMR_PATIENT_NDC ND" +
                   " where ND.PATIENT_KEY = " + patientKey + "";
            // return "01-JAN-2008";
        }
        else
        {
            strSQL = "select MAX(DG.CODEDATE) as MaxDate  " +
                        " from UMR_CLASSIFIER C " +
                        " INNER JOIN UMR_MEDCODES MC on C.GUID = MC.GUID  " +
                        " INNER JOIN UMR_PATIENT_DIAGNOSES DG on MC.MEDCODE=DG.MEDCODE  " +
                        " where C.DISCODE=" + discode + " and DG.PATIENT_KEY =" + patientKey + " ";
        }

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.CommandType = CommandType.Text;

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            return dt.Rows[0]["MaxDate"].ToString();
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }

    public DataSet GetLegendByUserID(string UserId)
    {
        string spName = "sp_LegendGetByUserID";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmUserId = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        //Set values in the parameters
        prmUserId.Value = UserId;


        //Add the parameters in...
        cmd.Parameters.Add(prmUserId);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public void UpdateLegend(string userID, DateTime dayRange1, int dayRange2, int dayRange3, int dayRange4)
    {
        string spName = "sp_LegendEdit";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmUserID = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        SqlParameter prmDayRange1 = new SqlParameter("p_FirstDate", SqlDbType.DateTime);
        SqlParameter prmDayRange2 = new SqlParameter("p_DayRange2", SqlDbType.Int);
        SqlParameter prmDayRange3 = new SqlParameter("p_DayRange3", SqlDbType.Int);
        SqlParameter prmDayRange4 = new SqlParameter("p_DayRange4", SqlDbType.Int);

        //Set values in the parameters
        prmUserID.Value = userID;
        prmDayRange1.Value = dayRange1;
        prmDayRange2.Value = dayRange2;
        prmDayRange3.Value = dayRange3;
        prmDayRange4.Value = dayRange4;

        //Add the parameters in...
        cmd.Parameters.Add(prmUserID);
        cmd.Parameters.Add(prmDayRange1);
        cmd.Parameters.Add(prmDayRange2);
        cmd.Parameters.Add(prmDayRange3);
        cmd.Parameters.Add(prmDayRange4);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (SqlException Ex) { LogError(Ex); throw; }
        catch (Exception ex) { LogError(ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    //public void UpdateLegend(string userID, int dayRange1, int dayRange2, int dayRange3, int dayRange4)
    //{
    //    string spName = "sp_LegendEdit";

    //    //Create the SQL Command
    //    SqlCommand cmd = new SqlCommand(spName, con);
    //    cmd.CommandType = CommandType.StoredProcedure;

    //    //Create the SQL Parameters
    //    SqlParameter prmUserID = new SqlParameter("p_UserID", SqlDbType.NVarChar);
    //    SqlParameter prmDayRange1 = new SqlParameter("p_DayRange1", SqlDbType.Int);
    //    SqlParameter prmDayRange2 = new SqlParameter("p_DayRange2", SqlDbType.Int);
    //    SqlParameter prmDayRange3 = new SqlParameter("p_DayRange3", SqlDbType.Int);
    //    SqlParameter prmDayRange4 = new SqlParameter("p_DayRange4", SqlDbType.Int);

    //    //Set values in the parameters
    //    prmUserID.Value = userID;
    //    prmDayRange1.Value = dayRange1;
    //    prmDayRange2.Value = dayRange2;
    //    prmDayRange3.Value = dayRange3;
    //    prmDayRange4.Value = dayRange4;

    //    //Add the parameters in...
    //    cmd.Parameters.Add(prmUserID);
    //    cmd.Parameters.Add(prmDayRange1);
    //    cmd.Parameters.Add(prmDayRange2);
    //    cmd.Parameters.Add(prmDayRange3);
    //    cmd.Parameters.Add(prmDayRange4);

    //    try
    //    {
    //        cmd.Connection.Open();
    //        cmd.ExecuteNonQuery();
    //        cmd.Connection.Close();
    //    }
    //    catch (SqlException Ex) { LogError(Ex); throw; }
    //    catch (Exception ex) { LogError(ex); throw; }
    //    finally
    //    {
    //        if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
    //        {
    //            cmd.Connection.Close();
    //        }
    //    }
    //}
    public void LegendAdNew(string userID, DateTime dayRange1, int dayRange2, int dayRange3, int dayRange4)
    {
        string spName = "sp_LegendAddNew";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmUserID = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        SqlParameter prmDayRange1 = new SqlParameter("p_FirstDate", SqlDbType.DateTime);
        SqlParameter prmDayRange2 = new SqlParameter("p_DayRange2", SqlDbType.Int);
        SqlParameter prmDayRange3 = new SqlParameter("p_DayRange3", SqlDbType.Int);
        SqlParameter prmDayRange4 = new SqlParameter("p_DayRange4", SqlDbType.Int);

        //Set values in the parameters
        prmUserID.Value = userID;
        prmDayRange1.Value = dayRange1;
        prmDayRange2.Value = dayRange2;
        prmDayRange3.Value = dayRange3;
        prmDayRange4.Value = dayRange4;

        //Add the parameters in...
        cmd.Parameters.Add(prmUserID);
        cmd.Parameters.Add(prmDayRange1);
        cmd.Parameters.Add(prmDayRange2);
        cmd.Parameters.Add(prmDayRange3);
        cmd.Parameters.Add(prmDayRange4);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (SqlException Ex) { LogError(Ex); throw; }
        catch (Exception ex) { LogError(ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    //public void LegendAdNew2(string userID, int dayRange1, int dayRange2, int dayRange3, int dayRange4)
    //{
    //    string spName = "sp_LegendAddNew";

    //    //Create the SQL Command
    //    SqlCommand cmd = new SqlCommand(spName, con);
    //    cmd.CommandType = CommandType.StoredProcedure;

    //    //Create the SQL Parameters
    //    SqlParameter prmUserID = new SqlParameter("p_UserID", SqlDbType.NVarChar);
    //    SqlParameter prmDayRange1 = new SqlParameter("p_DayRange1", SqlDbType.Int);
    //    SqlParameter prmDayRange2 = new SqlParameter("p_DayRange2", SqlDbType.Int);
    //    SqlParameter prmDayRange3 = new SqlParameter("p_DayRange3", SqlDbType.Int);
    //    SqlParameter prmDayRange4 = new SqlParameter("p_DayRange4", SqlDbType.Int);

    //    //Set values in the parameters
    //    prmUserID.Value = userID;
    //    prmDayRange1.Value = dayRange1;
    //    prmDayRange2.Value = dayRange2;
    //    prmDayRange3.Value = dayRange3;
    //    prmDayRange4.Value = dayRange4;

    //    //Add the parameters in...
    //    cmd.Parameters.Add(prmUserID);
    //    cmd.Parameters.Add(prmDayRange1);
    //    cmd.Parameters.Add(prmDayRange2);
    //    cmd.Parameters.Add(prmDayRange3);
    //    cmd.Parameters.Add(prmDayRange4);

    //    try
    //    {
    //        cmd.Connection.Open();
    //        cmd.ExecuteNonQuery();
    //        cmd.Connection.Close();
    //    }
    //    catch (SqlException Ex) { LogError(Ex); throw; }
    //    catch (Exception ex) { LogError(ex); throw; }
    //    finally
    //    {
    //        if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
    //        {
    //            cmd.Connection.Close();
    //        }
    //    }
    //}

    public DataSet GetInstitutionInfo(string institutionID)
    {
        string spName = "sp_InstitutionGetByPK";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmInstitutionID = new SqlParameter("p_InstitutionID", SqlDbType.NVarChar);
        //Set values in the parameters
        prmInstitutionID.Value = institutionID;

        //Add the parameters in...
        cmd.Parameters.Add(prmInstitutionID);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetInstitutionsForPatients(long patientKey)
    {
        return GetInstitutionsForPatients(patientKey, null, null);
    }
    public DataSet GetInstitutionsForPatients(long patientKey, DateTime? fromDate, DateTime? toDate)
    {
        string spName = "sp_InstitutionsGetByPatient";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmFromDate = new SqlParameter("p_FromDate", SqlDbType.DateTime);
        SqlParameter prmToDate = new SqlParameter("p_ToDate", SqlDbType.DateTime);

        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        if (fromDate != null)
        {
            prmFromDate.Value = fromDate.Value;
        }
        if (toDate != null)
        {
            prmToDate.Value = toDate.Value;
        }

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmFromDate);
        cmd.Parameters.Add(prmToDate);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataTable GetLastDateAndFirstDate(long patientKey)
    {
        string strSQL = " SELECT * FROM(SELECT  MAX(CodeDate) as LastDate, MIN(CodeDate) AS FirstDate " +
                        " FROM(" +
                        " SELECT D.CodeDate FROM UMR_Patient_Diagnoses D" +
                                " WHERE D.PATIENT_KEY = " + patientKey + " " +
                        " UNION" +
                        " SELECT N.CodeDate FROM UMR_Patient_NDC N" +
                                " WHERE N.PATIENT_KEY = " + patientKey + " " +
                        " ) T) T2 WHERE T2.LASTDATE is not null AND T2.FirstDate is not null";


        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.CommandType = CommandType.Text;

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public void UpdatePatientAccessRecordUpdateTime(long patientKey, string userid, DateTime lastAccessTime)
    {
        string spName = "sp_PatientAccessUpdate";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmUserId = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        SqlParameter prmAccessDate = new SqlParameter("p_AccessDate", SqlDbType.DateTime);

        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        prmUserId.Value = userid;
        prmAccessDate.Value = lastAccessTime;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmUserId);
        cmd.Parameters.Add(prmAccessDate);

        try
        {
            cmd.Connection.Open();
            int x = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (SqlException Ex) { LogError(Ex); throw; }
        catch (Exception ex) { LogError(ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetCodeModifiersByMedcode(string codeType, string medcode, int codeVersion)
    {
        string spName = "SP_CODEMODIFIERGETBYMEDCODE";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmCodeType = new SqlParameter("p_CodeType", SqlDbType.NVarChar);
        SqlParameter prmMedCode = new SqlParameter("p_MedCode", SqlDbType.NVarChar);
        SqlParameter prmCodeVersion = new SqlParameter("p_CodeVersion", SqlDbType.Int);


        //Set values in the parameters
        prmCodeType.Value = codeType;
        prmMedCode.Value = medcode;
        prmCodeVersion.Value = codeVersion;

        //Add the parameters in...
        cmd.Parameters.Add(prmCodeType);
        cmd.Parameters.Add(prmMedCode);
        cmd.Parameters.Add(prmCodeVersion);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetUsagesLogins()
    {
        string spName = "SP_USAGELOGINGET";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetUsagesLoginsByUserID(string userId)
    {
        string spName = "SP_USAGELOGINGETBYUSER";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;


        //Create the SQL Parameters
        SqlParameter prmUserID = new SqlParameter("p_UserID", SqlDbType.NVarChar);

        //Set values in the parameters
        prmUserID.Value = userId;

        //Add the parameters in...
        cmd.Parameters.Add(prmUserID);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataSet GetUsagesPatientAccess(string userId, DateTime min, DateTime max)
    {
        string spName = "SP_USAGEPAIENTACCESS";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;


        //Create the SQL Parameters
        SqlParameter prmUserID = new SqlParameter("p_UserID", SqlDbType.NVarChar);
        SqlParameter prmMindate = new SqlParameter("p_minDate", SqlDbType.DateTime);
        SqlParameter prmMaxDate = new SqlParameter("p_maxDate", SqlDbType.DateTime);

        //Set values in the parameters
        prmUserID.Value = userId;
        prmMindate.Value = min;
        prmMaxDate.Value = max;

        //Add the parameters in...
        cmd.Parameters.Add(prmUserID);
        cmd.Parameters.Add(prmMindate);
        cmd.Parameters.Add(prmMaxDate);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public void UsagesLoginAdd(string userId)
    {
        string spName = "SP_USAGELOGINADD";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmUserId = new SqlParameter("p_UserID", SqlDbType.NVarChar);

        //Set values in the parameters
        prmUserId.Value = userId;

        //Add the parameters in...
        cmd.Parameters.Add(prmUserId);

        try
        {
            cmd.Connection.Open();
            int x = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (SqlException Ex) { LogError(Ex); throw; }
        catch (Exception ex) { LogError(ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataTable GetPatientSearchPageData(string query, int currentPage, int pageSize, int option)
    {
        string spName = "SP_PatientSearchPageData";
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmQuery = new SqlParameter("Query", SqlDbType.NVarChar);
        SqlParameter prmCurrentPage = new SqlParameter("CurrentPage", SqlDbType.Int);
        SqlParameter prmPageSize = new SqlParameter("PageSize", SqlDbType.Int);
        SqlParameter prmOption = new SqlParameter("Option", SqlDbType.Int);


        prmQuery.Value = query;
        prmCurrentPage.Value = currentPage;
        prmPageSize.Value = pageSize;
        prmOption.Value = option;

        cmd.Parameters.Add(prmQuery);
        cmd.Parameters.Add(prmCurrentPage);
        cmd.Parameters.Add(prmPageSize);
        cmd.Parameters.Add(prmOption);

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public int GetPatientSearchPageCount(string query, int pageSize, int option)
    {
        int count = 0;
        string spName = "SP_PatientSearchPageCount";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmQuery = new SqlParameter("Query", SqlDbType.NVarChar);
        SqlParameter prmPageSize = new SqlParameter("PageSize", SqlDbType.Int);
        SqlParameter prmOption = new SqlParameter("Option", SqlDbType.NVarChar);

        //Set values in the parameters
        prmQuery.Value = query;
        prmPageSize.Value = pageSize;
        prmOption.Value = option;

        //Add the parameters in...
        cmd.Parameters.Add(prmQuery);
        cmd.Parameters.Add(prmPageSize);
        cmd.Parameters.Add(prmOption);
        try
        {
            cmd.Connection.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
        }
        catch (Exception ex) { LogError(ex); return -1; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return count;
    }
    public int GetPatientSearchRecordCount(string query, int option)
    {
        int count = 0;
        string spName = "SP_PatientSearchRecordCount";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmQuery = new SqlParameter("Query", SqlDbType.NVarChar);
        SqlParameter prmOption = new SqlParameter("Option", SqlDbType.NVarChar);

        //Set values in the parameters
        prmQuery.Value = query;
        prmOption.Value = option;

        //Add the parameters in...
        cmd.Parameters.Add(prmQuery);
        cmd.Parameters.Add(prmOption);
        try
        {
            cmd.Connection.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
        }
        catch (Exception ex) { LogError(ex); return -1; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return count;
    }
    public DataTable GetPatientEducation(long patientKey)
    {
        string spName = "SP_EDUCATIONGETBYPATIENT";
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmPatientKey = new SqlParameter("@p_PatientKey", SqlDbType.BigInt);

        prmPatientKey.Value = patientKey;

        cmd.Parameters.Add(prmPatientKey);

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataTable GetPatientEmployment(long patientKey)
    {
        string spName = "SP_EMPLOYMENTGETBYPATIENT";
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmPatientKey = new SqlParameter("@p_PatientKey", SqlDbType.BigInt);

        prmPatientKey.Value = patientKey;

        cmd.Parameters.Add(prmPatientKey);

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public int GetAnalyzerCommonDiagnosesCount(char sex, int ageFrom, int ageTo)
    {
        int count = 0;
        string spName = "SP_ANALYZER_COMMON_DIAGNOSES_COUNT";
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmSex = new SqlParameter("@p_Sex", SqlDbType.Char);
        SqlParameter prmAgeFrom = new SqlParameter("@p_AgeFrom", SqlDbType.Int);
        SqlParameter prmAgeTo = new SqlParameter("@p_AgeTo", SqlDbType.Int);

        prmSex.Value = sex;
        prmAgeFrom.Value = ageFrom;
        prmAgeTo.Value = ageTo;

        cmd.Parameters.Add(prmSex);
        cmd.Parameters.Add(prmAgeFrom);
        cmd.Parameters.Add(prmAgeTo);

        //Create the DataTable
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
        }
        catch (Exception ex) { LogError(ex); }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return count;
    }
    public DataSet GetAnalyzerCommonDiagnoses(char sex, int ageFrom, int ageTo, int pageSize, int currentPage)
    {
        string spName = "SP_ANALYZER_COMMON_DIAGNOSES";
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmSex = new SqlParameter("@p_Sex", SqlDbType.Char);
        SqlParameter prmAgeFrom = new SqlParameter("@p_AgeFrom", SqlDbType.Int);
        SqlParameter prmAgeTo = new SqlParameter("@p_AgeTo", SqlDbType.Int);
        SqlParameter prmCurrentPage = new SqlParameter("@P_CurrentPage", SqlDbType.Int);
        SqlParameter prmPageSize = new SqlParameter("@P_PageSize", SqlDbType.Int);

        prmSex.Value = sex;
        prmAgeFrom.Value = ageFrom;
        prmAgeTo.Value = ageTo;
        prmCurrentPage.Value = currentPage;
        prmPageSize.Value = pageSize;

        cmd.Parameters.Add(prmSex);
        cmd.Parameters.Add(prmAgeFrom);
        cmd.Parameters.Add(prmAgeTo);
        cmd.Parameters.Add(prmCurrentPage);
        cmd.Parameters.Add(prmPageSize);

        //Create the DataTable
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public int GetAnalyzerPatientByMedcodeAgeSexCount(char sex, int ageFrom, int ageTo, string code_type, string medcode, int codeversion)
    {
        int count = 0;
        string spName = "SP_ANALYZER_PATIENT_BY_MEDCODE_AGE_SEX_COUNT";
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmSex = new SqlParameter("@p_Sex", SqlDbType.Char);
        SqlParameter prmAgeFrom = new SqlParameter("@p_AgeFrom", SqlDbType.Int);
        SqlParameter prmAgeTo = new SqlParameter("@p_AgeTo", SqlDbType.Int);
        SqlParameter prmCodeType = new SqlParameter("@p_CodeType", SqlDbType.NVarChar);
        SqlParameter prmMedcode = new SqlParameter("@p_Medcode", SqlDbType.NVarChar);
        SqlParameter prmCodeVersion = new SqlParameter("@p_CodeVersion", SqlDbType.Int);

        prmSex.Value = sex;
        prmAgeFrom.Value = ageFrom;
        prmAgeTo.Value = ageTo;
        prmCodeType.Value = code_type;
        prmMedcode.Value = medcode;
        prmCodeVersion.Value = codeversion;

        cmd.Parameters.Add(prmSex);
        cmd.Parameters.Add(prmAgeFrom);
        cmd.Parameters.Add(prmAgeTo);
        cmd.Parameters.Add(prmCodeType);
        cmd.Parameters.Add(prmMedcode);
        cmd.Parameters.Add(prmCodeVersion);

        //Create the DataTable
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
        }
        catch (Exception ex) { LogError(ex); }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return count;
    }
    public DataTable GetAnalyzerPatientByMedcodeAgeSex(char sex, int ageFrom, int ageTo, string code_type, string medcode, int codeversion, int pageSize, int currentPage)
    {
        string spName = "SP_ANALYZER_PATIENT_BY_MEDCODE_AGE_SEX";
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmSex = new SqlParameter("@p_Sex", SqlDbType.Char);
        SqlParameter prmAgeFrom = new SqlParameter("@p_AgeFrom", SqlDbType.Int);
        SqlParameter prmAgeTo = new SqlParameter("@p_AgeTo", SqlDbType.Int);
        SqlParameter prmCodeType = new SqlParameter("@p_CodeType", SqlDbType.NVarChar);
        SqlParameter prmMedcode = new SqlParameter("@p_Medcode", SqlDbType.NVarChar);
        SqlParameter prmCodeVersion = new SqlParameter("@p_CodeVersion", SqlDbType.Int);
        SqlParameter prmCurrentPage = new SqlParameter("@P_CurrentPage", SqlDbType.Int);
        SqlParameter prmPageSize = new SqlParameter("@P_PageSize", SqlDbType.Int);

        prmSex.Value = sex;
        prmAgeFrom.Value = ageFrom;
        prmAgeTo.Value = ageTo;
        prmCodeType.Value = code_type;
        prmMedcode.Value = medcode;
        prmCodeVersion.Value = codeversion;
        prmCurrentPage.Value = currentPage;
        prmPageSize.Value = pageSize;

        cmd.Parameters.Add(prmSex);
        cmd.Parameters.Add(prmAgeFrom);
        cmd.Parameters.Add(prmAgeTo);
        cmd.Parameters.Add(prmCodeType);
        cmd.Parameters.Add(prmMedcode);
        cmd.Parameters.Add(prmCodeVersion);
        cmd.Parameters.Add(prmCurrentPage);
        cmd.Parameters.Add(prmPageSize);

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataTable GetCOdeDescriptinChangeHistory(string codeType, string medcode)
    {
        string spName = "SP_CODE_DESCRIPTION_HISTORY";
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmCodeType = new SqlParameter("@p_CodeType", SqlDbType.NVarChar);
        SqlParameter prmMedcode = new SqlParameter("@p_MedCode", SqlDbType.NVarChar);

        prmCodeType.Value = codeType;
        prmMedcode.Value = medcode;

        cmd.Parameters.Add(prmCodeType);
        cmd.Parameters.Add(prmMedcode);

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }
    public DataTable GetCodeHistory(string codeType, string medcode)
    {
        string spName = "SP_CODE_HISTORY";
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmCodeType = new SqlParameter("@p_CodeType", SqlDbType.NVarChar);
        SqlParameter prmMedcode = new SqlParameter("@p_MedCode", SqlDbType.NVarChar);

        prmCodeType.Value = codeType;
        prmMedcode.Value = medcode;

        cmd.Parameters.Add(prmCodeType);
        cmd.Parameters.Add(prmMedcode);

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }




    #region Nurse Related
    public DataSet GetNurseList(string ID, string FName, string LName, int currentPageIndex, int pageSize, string orderBy)
    {
        string spName = "SP_NurseGetForPaging";

        //Create the SQLCommand Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        //P_CurrentPage INT, @P_PageSize INT,
        SqlParameter prmStartIndex = new SqlParameter("P_CurrentPage", SqlDbType.Int);
        SqlParameter prmPageSize = new SqlParameter("P_PageSize", SqlDbType.Int);
        SqlParameter prmOrderBy = new SqlParameter("p_OrderBy", SqlDbType.NVarChar);

        SqlParameter prmID = new SqlParameter("p_NurseID", SqlDbType.NVarChar);
        SqlParameter prmFName = new SqlParameter("p_FirstName", SqlDbType.NVarChar);
        SqlParameter prmLName = new SqlParameter("p_LastName", SqlDbType.NVarChar);

        //Set values in the parameters
        prmStartIndex.Value = currentPageIndex;
        prmPageSize.Value = pageSize;
        prmOrderBy.Value = orderBy.Trim().Length == 0 ? null : orderBy;

        prmID.Value = ID.Trim().Length == 0 ? null : ID;
        prmFName.Value = FName.Trim().Length == 0 ? null : FName;
        prmLName.Value = LName.Trim().Length == 0 ? null : LName;

        //Add the parameters in...
        cmd.Parameters.Add(prmStartIndex);
        cmd.Parameters.Add(prmPageSize);
        cmd.Parameters.Add(prmOrderBy);

        cmd.Parameters.Add(prmID);
        cmd.Parameters.Add(prmFName);
        cmd.Parameters.Add(prmLName);

        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            cmd.Connection.Open();
            da.Fill(ds);
            cmd.Connection.Close();
        }
        catch (Exception ex) { LogError(ex); }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return ds;
    }

    public int GetNurseCount(string ID, string FName, string LName)
    {
        string spName = "SP_NurseGetForPagingCount";

        //Create the SQLCommand Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmID = new SqlParameter("p_NurseID", SqlDbType.NVarChar);
        SqlParameter prmFName = new SqlParameter("p_FirstName", SqlDbType.NVarChar);
        SqlParameter prmLName = new SqlParameter("p_LastName", SqlDbType.NVarChar);

        //Set values in the parameters
        prmID.Value = ID.Trim().Length == 0 ? null : ID;
        prmFName.Value = FName.Trim().Length == 0 ? null : FName;
        prmLName.Value = LName.Trim().Length == 0 ? null : LName;


        //Add the parameters in...
        cmd.Parameters.Add(prmID); cmd.Parameters.Add(prmFName);
        cmd.Parameters.Add(prmLName); 

        int count = 0;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            cmd.Connection.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
        }
        catch (Exception ex) { LogError(ex); }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return count;
    }

    public DataTable GetNurseSearchPageData(string query, int currentPage, int pageSize, int option)
    {
        string spName = "SP_NurseSearchPageData";
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the Orale Parameters
        SqlParameter prmQuery = new SqlParameter("Query", SqlDbType.NVarChar);
        SqlParameter prmCurrentPage = new SqlParameter("CurrentPage", SqlDbType.Int);
        SqlParameter prmPageSize = new SqlParameter("PageSize", SqlDbType.Int);
        SqlParameter prmOption = new SqlParameter("Option", SqlDbType.Int);


        prmQuery.Value = query;
        prmCurrentPage.Value = currentPage;
        prmPageSize.Value = pageSize;
        prmOption.Value = option;

        cmd.Parameters.Add(prmQuery);
        cmd.Parameters.Add(prmCurrentPage);
        cmd.Parameters.Add(prmPageSize);
        cmd.Parameters.Add(prmOption);

        //Create the DataTable
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }

    public int GetNurseSearchPageCount(string query, int pageSize, int option)
    {
        int count = 0;
        string spName = "SP_NurseSearchPageCount";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmQuery = new SqlParameter("Query", SqlDbType.NVarChar);
        SqlParameter prmPageSize = new SqlParameter("PageSize", SqlDbType.Int);
        SqlParameter prmOption = new SqlParameter("Option", SqlDbType.NVarChar);

        //Set values in the parameters
        prmQuery.Value = query;
        prmPageSize.Value = pageSize;
        prmOption.Value = option;

        //Add the parameters in...
        cmd.Parameters.Add(prmQuery);
        cmd.Parameters.Add(prmPageSize);
        cmd.Parameters.Add(prmOption);
        try
        {
            cmd.Connection.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
        }
        catch (Exception ex) { LogError(ex); return -1; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return count;
    }

    public int GetNurseSearchRecordCount(string query, int option)
    {
        int count = 0;
        string spName = "SP_NurseSearchRecordCount";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmQuery = new SqlParameter("Query", SqlDbType.NVarChar);
        SqlParameter prmOption = new SqlParameter("Option", SqlDbType.NVarChar);

        //Set values in the parameters
        prmQuery.Value = query;
        prmOption.Value = option;

        //Add the parameters in...
        cmd.Parameters.Add(prmQuery);
        cmd.Parameters.Add(prmOption);
        try
        {
            cmd.Connection.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
        }
        catch (Exception ex) { LogError(ex); return -1; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return count;
    }

    public DataSet GetNurses()
    {

        string query = string.Format(@"SELECT [NURSE_ID]
                                        ,[LASTNAME]
                                        ,[FIRSTNAME]
                                        ,[PHONE]
                                        ,[FAX]
                                        ,[CELL_PHONE]
                                        ,[PAGER]
                                        ,[INSTITUTION_ID]
                                        ,[WEBSITE]
                                        ,[EMAIL]
                                        ,[ADDRESS]
                                        ,[CITY_TOWN]
                                        ,[ZIP]
                                        ,[COUNTRY]
                                        ,[DEGREE]
                                        ,[NPI]
                                    FROM [UMRDB_NEW].[dbo].[UMR_NURSES]");

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;

        DataSet ds = new DataSet();
        try
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }

    }
    #endregion

    #region ALERT RELATED
    public int AddVitalSigns(long patientKey, DateTime date, string temperature, char tempUnit, string bloodPressureSys, string bloodPressureDia, string pulse, string pulseType, string respiratoryRate, string chiefComplain, string BMIWeight, string BMIHeight, string painScale, string painLocation, string pupilSizeRight, string pupilSizeLeft, string bloodGlucoseLevel, out string ErrorMessage)
    {
        string spName = "SP_ADD_VITALSIGNS";
        ErrorMessage = "";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmPatientKey = new SqlParameter("p_PatientKey", SqlDbType.BigInt);
        SqlParameter prmDate = new SqlParameter("p_Date", SqlDbType.DateTime);
        SqlParameter prmTemperature = new SqlParameter("p_Temperature", SqlDbType.NVarChar);
        SqlParameter prmTemperatureUnit = new SqlParameter("p_TemperatureUnit", SqlDbType.NVarChar);
        SqlParameter prmBloodPressureSys = new SqlParameter("p_BloodPressureSys", SqlDbType.NVarChar);
        SqlParameter prmBloodPressureDia = new SqlParameter("p_BloodPressureDia", SqlDbType.NVarChar);
        SqlParameter prmPulse = new SqlParameter("p_Pulse", SqlDbType.NVarChar);
        SqlParameter prmPulseType = new SqlParameter("p_PulseType", SqlDbType.NVarChar);
        SqlParameter prmRespiratoryRate = new SqlParameter("p_RespiratoryRate", SqlDbType.NVarChar);
        SqlParameter prmBMIWeight = new SqlParameter("p_BMIWight", SqlDbType.NVarChar);
        SqlParameter prmBMIHeight = new SqlParameter("p_BMIHeight", SqlDbType.NVarChar);
        SqlParameter prmChiefComplain = new SqlParameter("p_ChiefComplain", SqlDbType.NVarChar);

        SqlParameter prmPainScale = new SqlParameter("p_PainScale", SqlDbType.NVarChar);
        SqlParameter prmPainLocation = new SqlParameter("p_PainLocation", SqlDbType.NVarChar);
        SqlParameter prmPupilSizeRight = new SqlParameter("p_PupilSizeRight", SqlDbType.NVarChar);
        SqlParameter prmPupilSizeLeft = new SqlParameter("p_PupilSizeLeft", SqlDbType.NVarChar);
        SqlParameter prmBloodGlucoseLevel = new SqlParameter("p_BloodGlucoseLevel", SqlDbType.NVarChar);

        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        prmDate.Value = date;
        prmTemperature.Value = temperature;
        prmTemperatureUnit.Value = tempUnit;
        prmBloodPressureSys.Value = bloodPressureSys;
        prmBloodPressureDia.Value = bloodPressureDia;
        prmPulse.Value = pulse;
        prmPulseType.Value = pulseType;
        prmRespiratoryRate.Value = respiratoryRate;
        prmChiefComplain.Value = chiefComplain;
        prmBMIWeight.Value = BMIWeight;
        prmBMIHeight.Value = BMIHeight;

        prmPainScale.Value = painScale;
        prmPainLocation.Value = painLocation;
        prmPupilSizeRight.Value = pupilSizeRight;
        prmPupilSizeLeft.Value = pupilSizeLeft;
        prmBloodGlucoseLevel.Value = bloodGlucoseLevel;

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmDate);
        cmd.Parameters.Add(prmTemperature);
        cmd.Parameters.Add(prmTemperatureUnit);
        cmd.Parameters.Add(prmBloodPressureSys);
        cmd.Parameters.Add(prmBloodPressureDia);
        cmd.Parameters.Add(prmPulse);
        cmd.Parameters.Add(prmPulseType);
        cmd.Parameters.Add(prmRespiratoryRate);
        cmd.Parameters.Add(prmChiefComplain);
        cmd.Parameters.Add(prmBMIWeight);
        cmd.Parameters.Add(prmBMIHeight);
        cmd.Parameters.Add(prmPainScale);
        cmd.Parameters.Add(prmPainLocation);
        cmd.Parameters.Add(prmPupilSizeRight);
        cmd.Parameters.Add(prmPupilSizeLeft);
        cmd.Parameters.Add(prmBloodGlucoseLevel);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (SqlException Ex)
        {
            LogError(Ex);

            ErrorMessage = "An Unspecified Error Occurred[Error Code : " + Ex.ToString() + "]. Please contact your administrator with the error code to resolve this issue";

            return -1;
        }
        catch (Exception Ex) { LogError(Ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return 1;
    }

    #region AddVitalSigns
    public int AddVitalSigns(long patientKey,
        DateTime record_date,
        string temperature,
        char tempUnit,
        string bloodPressureSys,
        string bloodPressureDia,
        string pulse,
        string pulseType,
        string respiratoryRate,
        string chiefComplain,
        string BMIWeight,
        string BMIHeight,
        string painScale,
        string painLocation,
        string pupilSizeRight,
        string pupilSizeLeft,
        string bloodGlucoseLevel,
        byte[] patient_image,
        out int identiy,
        out string ErrorMessage)
    {
        #region SP FILEDS
        /*
        @p_patient_key bigint, 
	    @p_record_date datetime, 
	    @p_chief_complaint nvarchar(2000), 
	    @p_temperature nvarchar(50), 
	    @p_temperature_unit char(1), 
	    @p_blood_pressure_systolic nvarchar(200), 
	    @p_blood_pressure_diastolic nvarchar(200), 
	    @p_pulse nvarchar(50), 
	    @p_pulse_type nvarchar(50), 
	    @p_respiratory_rate nvarchar(100), 
	    @p_bmi_weight nvarchar(100), 
	    @p_bmi_height nvarchar(100), 
	    @p_pain_scale nvarchar(100), 
	    @p_pain_location nvarchar(500), 
	    @p_pupil_size_right nvarchar(100), 
	    @p_pupil_size_left nvarchar(100), 
	    @p_blood_glucose_level nvarchar(500), 
	    @p_patient_image image,
        @Identity int OUT
        */
        #endregion

        string spName = "SP_ADD_VITALSIGNS";
        ErrorMessage = "";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmPatientKey = new SqlParameter("@p_patient_key", SqlDbType.BigInt);
        SqlParameter prmDate = new SqlParameter("@p_record_date", SqlDbType.DateTime);
        SqlParameter prmTemperature = new SqlParameter("@p_temperature", SqlDbType.NVarChar);
        SqlParameter prmTemperatureUnit = new SqlParameter("@p_temperature_unit", SqlDbType.NVarChar);
        SqlParameter prmBloodPressureSys = new SqlParameter("@p_blood_pressure_systolic", SqlDbType.NVarChar);
        SqlParameter prmBloodPressureDia = new SqlParameter("@p_blood_pressure_diastolic", SqlDbType.NVarChar);
        SqlParameter prmPulse = new SqlParameter("@p_pulse", SqlDbType.NVarChar);
        SqlParameter prmPulseType = new SqlParameter("@p_pulse_type", SqlDbType.NVarChar);
        SqlParameter prmRespiratoryRate = new SqlParameter("@p_respiratory_rate", SqlDbType.NVarChar);
        SqlParameter prmBMIWeight = new SqlParameter("@p_bmi_weight", SqlDbType.NVarChar);
        SqlParameter prmBMIHeight = new SqlParameter("@p_bmi_height", SqlDbType.NVarChar);
        SqlParameter prmChiefComplain = new SqlParameter("@p_chief_complaint", SqlDbType.NVarChar);

        SqlParameter prmPainScale = new SqlParameter("@p_pain_scale", SqlDbType.NVarChar);
        SqlParameter prmPainLocation = new SqlParameter("@p_pain_location", SqlDbType.NVarChar);
        SqlParameter prmPupilSizeRight = new SqlParameter("@p_pupil_size_right", SqlDbType.NVarChar);
        SqlParameter prmPupilSizeLeft = new SqlParameter("@p_pupil_size_left", SqlDbType.NVarChar);
        SqlParameter prmBloodGlucoseLevel = new SqlParameter("@p_blood_glucose_level", SqlDbType.NVarChar);
        SqlParameter prmPatientImage = new SqlParameter("@p_patient_image", SqlDbType.Image);


        SqlParameter prmIdentity = new SqlParameter("@Identity", SqlDbType.Int);
        prmIdentity.Direction = ParameterDirection.Output;

        //Set values in the parameters
        prmPatientKey.Value = patientKey;
        prmDate.Value = record_date;
        prmTemperature.Value = temperature;
        prmTemperatureUnit.Value = tempUnit;
        prmBloodPressureSys.Value = bloodPressureSys;
        prmBloodPressureDia.Value = bloodPressureDia;
        prmPulse.Value = pulse;
        prmPulseType.Value = pulseType;
        prmRespiratoryRate.Value = respiratoryRate;
        prmChiefComplain.Value = chiefComplain;
        prmBMIWeight.Value = BMIWeight;
        prmBMIHeight.Value = BMIHeight;

        prmPainScale.Value = painScale;
        prmPainLocation.Value = painLocation;
        prmPupilSizeRight.Value = pupilSizeRight;
        prmPupilSizeLeft.Value = pupilSizeLeft;
        prmBloodGlucoseLevel.Value = bloodGlucoseLevel;

        if (patient_image != null)
        {
            prmPatientImage.Value = patient_image;
        }
        else
        {
            prmPatientImage.Value = DBNull.Value;
        }
        

        //Add the parameters in...
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmDate);
        cmd.Parameters.Add(prmTemperature);
        cmd.Parameters.Add(prmTemperatureUnit);
        cmd.Parameters.Add(prmBloodPressureSys);
        cmd.Parameters.Add(prmBloodPressureDia);
        cmd.Parameters.Add(prmPulse);
        cmd.Parameters.Add(prmPulseType);
        cmd.Parameters.Add(prmRespiratoryRate);
        cmd.Parameters.Add(prmChiefComplain);
        cmd.Parameters.Add(prmBMIWeight);
        cmd.Parameters.Add(prmBMIHeight);
        cmd.Parameters.Add(prmPainScale);
        cmd.Parameters.Add(prmPainLocation);
        cmd.Parameters.Add(prmPupilSizeRight);
        cmd.Parameters.Add(prmPupilSizeLeft);
        cmd.Parameters.Add(prmBloodGlucoseLevel);
        cmd.Parameters.Add(prmPatientImage);
        cmd.Parameters.Add(prmIdentity);

        identiy = 0;
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            identiy = (int)prmIdentity.Value;
            cmd.Connection.Close();
        }
        catch (SqlException Ex)
        {
            LogError(Ex);

            ErrorMessage = "An Unspecified Error Occurred[Error Code : " + Ex.ToString() + "]. Please contact your administrator with the error code to resolve this issue";

            return -1;
        }
        catch (Exception Ex) { LogError(Ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return 1;
    } 
    #endregion

    #region AddAlerts
    public int AddAlerts(DateTime nurse_soap_time_stamp,
        bool has_nurse_soap,
        byte nurse_soap_escalation_count,
        long patientKey,
        DateTime? doctor_soap_time_stamp,
        bool? has_doctor_soap,
        bool? is_dropped,
        byte alert_status,
        bool is_active,
        out long identiy,
        out string ErrorMessage)
    {
        #region SP FILEDS
       // @nurse_soap_time_stamp datetime,
       // @has_nurse_soap bit,
       // @nurse_soap_escalation_count tinyint,
       // @patient_key bigint,
       // @doctor_soap_time_stamp datetime,
       // @has_doctor_soap bit,
       // @is_dropped bit,
       //@alert_status tinyint,
       //@is_active bit,
        //@Identity bigint OUT
        #endregion

        string spName = "SP_ADD_ALERTS";
        ErrorMessage = "";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmNurseSoapTimeStamp = new SqlParameter("@nurse_soap_time_stamp", SqlDbType.DateTime);
        SqlParameter prmHasNurseSoap = new SqlParameter("@has_nurse_soap", SqlDbType.Bit);
        SqlParameter prmNurseSoapEscalationCount = new SqlParameter("@nurse_soap_escalation_count", SqlDbType.TinyInt);
        SqlParameter prmPatientKey = new SqlParameter("@patient_key", SqlDbType.BigInt);

        SqlParameter prmDoctorSoapTimeStamp = new SqlParameter("@doctor_soap_time_stamp", SqlDbType.DateTime);

        SqlParameter prmHasDoctorSoap = new SqlParameter("@has_doctor_soap", SqlDbType.Bit);
        SqlParameter prmIsDropped = new SqlParameter("@is_dropped", SqlDbType.Bit);
        SqlParameter prmAlertStatus = new SqlParameter("@alert_status", SqlDbType.TinyInt);
        SqlParameter prmIsActive = new SqlParameter("@is_active", SqlDbType.Bit);

        SqlParameter prmIdentity = new SqlParameter("@Identity", SqlDbType.BigInt);
        prmIdentity.Direction = ParameterDirection.Output;

        //Set values in the parameters
        prmNurseSoapTimeStamp.Value = nurse_soap_time_stamp;
        prmHasNurseSoap.Value = has_nurse_soap;
        prmNurseSoapEscalationCount.Value = nurse_soap_escalation_count;
        prmPatientKey.Value = patientKey;

        if (doctor_soap_time_stamp.HasValue)
        {
            prmDoctorSoapTimeStamp.Value = doctor_soap_time_stamp;
        }
        else
        {
            prmDoctorSoapTimeStamp.Value = DBNull.Value;
        }

        if (has_doctor_soap.HasValue)
        {
            prmHasDoctorSoap.Value = has_doctor_soap;
        }
        else
        {
            prmHasDoctorSoap.Value = DBNull.Value;
        }

        if (is_dropped.HasValue)
        {
            prmIsDropped.Value = is_dropped; 
        }
        else
        {
            prmIsDropped.Value = DBNull.Value;
        }

        prmAlertStatus.Value = alert_status;
        prmIsActive.Value = is_active;

        //Add the parameters in...
        cmd.Parameters.Add(prmNurseSoapTimeStamp);
        cmd.Parameters.Add(prmHasNurseSoap);
        cmd.Parameters.Add(prmNurseSoapEscalationCount);
        cmd.Parameters.Add(prmPatientKey);
        cmd.Parameters.Add(prmDoctorSoapTimeStamp);
        cmd.Parameters.Add(prmHasDoctorSoap);
        cmd.Parameters.Add(prmIsDropped);
        cmd.Parameters.Add(prmAlertStatus);
        cmd.Parameters.Add(prmIsActive);
        cmd.Parameters.Add(prmIdentity);

        identiy = 0;
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            identiy = (long)prmIdentity.Value;
            cmd.Connection.Close();
        }
        catch (SqlException Ex)
        {
            LogError(Ex);

            ErrorMessage = "An Unspecified Error Occurred[Error Code : " + Ex.ToString() + "]. Please contact your administrator with the error code to resolve this issue";

            return -1;
        }
        catch (Exception Ex) { LogError(Ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return 1;
    }
    #endregion

    #region AddSoaps
    public int AddSoaps(long? owner_id,
        long patient_key,
        DateTime date_added,
        string code_type,
        string doctor_id,
        string nurse_id,
        int recorded_vitalsigns_id,
        long alert_id,
        string subjective,
        string objective,
        string assessment,
        string notes,
        int? umr_plan_id,
        string doc_problems,
        string doc_prescription,
        string doc_diagnostictest,
        string doc_lab,
        string doc_procedures,
        string doc_immunization,
        string doc_pat_educations,
        string doc_respond,
        string doc_refer,
        bool examined_by_provider,
        string  doc_radiology,
        string doc_performance_measurement,
        string doc_emerging_tech_sv,
        string doc_other_text,
        string doc_followup,
        string doc_other,
        out long identiy,
        out string ErrorMessage)
    {
        #region SP FILEDS
        /*
	    @owner_id bigint, 1
	    @patient_key bigint, 2
	    @date_added datetime, 3
	    @code_type nvarchar(10), 4
	    @doctor_id nvarchar(15), 5
	    @nurse_id nvarchar(15), 6
	    @recorded_vitalsigns_id int, 7
	    @alert_id bigint, 8
	    @subjective nvarchar(2000), 9
	    @objective nvarchar(2000), 10
	    @assessment nvarchar(2000), 11
	    @notes nvarchar(2000), 12
	    @umr_plan_id int 13,
	    @doc_problems nvarchar(2000), 
	    @doc_prescription nvarchar(2000), 
	    @doc_diagnostictest nvarchar(2000), 
	    @doc_lab nvarchar(2000), 
	    @doc_procedures nvarchar(2000), 
	    @doc_immunization nvarchar(2000), 
	    @doc_pat_educations nvarchar(2000), 
	    @doc_respond nvarchar(2000), 
	    @doc_refer nvarchar(2000),
        @Identity bigint OUT
         */
        #endregion

        string spName = "SP_ADD_SOAPS";
        ErrorMessage = "";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prm_owner_id = new SqlParameter("@owner_id", SqlDbType.BigInt);
        SqlParameter prm_patient_key = new SqlParameter("@patient_key", SqlDbType.BigInt);
        SqlParameter prm_date_added = new SqlParameter("@date_added", SqlDbType.DateTime);
        SqlParameter prm_code_type = new SqlParameter("@code_type", SqlDbType.NVarChar);
        SqlParameter prm_doctor_id = new SqlParameter("@doctor_id", SqlDbType.NVarChar);
        SqlParameter prm_nurse_id = new SqlParameter("@nurse_id", SqlDbType.NVarChar);
        SqlParameter prm_recorded_vitalsigns_id = new SqlParameter("@recorded_vitalsigns_id", SqlDbType.Int);
        SqlParameter prm_alert_id = new SqlParameter("@alert_id", SqlDbType.BigInt);
        SqlParameter prm_subjective = new SqlParameter("@subjective", SqlDbType.NVarChar);
        SqlParameter prm_objective = new SqlParameter("@objective", SqlDbType.NVarChar);
        SqlParameter prm_assessment = new SqlParameter("@assessment", SqlDbType.NVarChar);
        SqlParameter prm_notes = new SqlParameter("@notes", SqlDbType.NVarChar);
        SqlParameter prm_umr_plan_id = new SqlParameter("@umr_plan_id", SqlDbType.Int);

        SqlParameter prm_doc_problems = new SqlParameter("@doc_problems", SqlDbType.NVarChar);
        SqlParameter prm_doc_prescription = new SqlParameter("@doc_prescription", SqlDbType.NVarChar);
        SqlParameter prm_doc_diagnostictest = new SqlParameter("@doc_diagnostictest", SqlDbType.NVarChar);
        SqlParameter prm_doc_lab = new SqlParameter("@doc_lab", SqlDbType.NVarChar);
        SqlParameter prm_doc_procedures = new SqlParameter("@doc_procedures", SqlDbType.NVarChar);
        SqlParameter prm_doc_immunization = new SqlParameter("@doc_immunization", SqlDbType.NVarChar);
        SqlParameter prm_doc_pat_educations = new SqlParameter("@doc_pat_educations", SqlDbType.NVarChar);
        SqlParameter prm_doc_respond = new SqlParameter("@doc_respond", SqlDbType.NVarChar);
        SqlParameter prm_doc_refer = new SqlParameter("@doc_refer", SqlDbType.NVarChar);


        SqlParameter prm_examined_by_provider = new SqlParameter("@is_examined_by_provider", SqlDbType.Bit);
        SqlParameter prm_doc_radiology = new SqlParameter("@doc_radiology", SqlDbType.NVarChar);
        SqlParameter prm_doc_performance_measurement = new SqlParameter("@doc_performance_measurement", SqlDbType.NVarChar);
        SqlParameter prm_doc_emerging_tech_sv = new SqlParameter("@doc_emerging_tech_sv", SqlDbType.NVarChar);
        SqlParameter prm_doc_other_text = new SqlParameter("@doc_other_text", SqlDbType.NVarChar);
        SqlParameter prm_doc_followup = new SqlParameter("@doc_followup", SqlDbType.NVarChar);
        SqlParameter prm_doc_other = new SqlParameter("@doc_other", SqlDbType.NVarChar);

        SqlParameter prmIdentity = new SqlParameter("@Identity", SqlDbType.BigInt);
        prmIdentity.Direction = ParameterDirection.Output;

        //Set values in the parameters
        if (owner_id.HasValue)
        {
            prm_owner_id.Value = owner_id;
        }
        else
        {
            prm_owner_id.Value = DBNull.Value;
        }

        prm_patient_key.Value = patient_key;
        prm_date_added.Value = date_added;

        if (code_type != null)
        {
            prm_code_type.Value = code_type;
        }
        else
        {
            prm_code_type.Value = DBNull.Value;
        }

        if (doctor_id != null)
        {
            prm_doctor_id.Value = doctor_id;
        }
        else
        {
            prm_doctor_id.Value = DBNull.Value;
        }

        if (nurse_id != null)
        {
            prm_nurse_id.Value = nurse_id;
        }
        else
        {
            prm_nurse_id.Value = DBNull.Value;
        }

        prm_recorded_vitalsigns_id.Value = recorded_vitalsigns_id;
        prm_alert_id.Value = alert_id;
        prm_subjective.Value = subjective;
        prm_objective.Value = objective;
        prm_assessment.Value = assessment;
        prm_notes.Value = notes;

        if (umr_plan_id.HasValue)
        {
            prm_umr_plan_id.Value = umr_plan_id;
        }
        else
        {
            prm_umr_plan_id.Value = DBNull.Value;
        }

        if (doc_problems != null)
        {
            prm_doc_problems.Value = doc_problems;
        }
        else
        {
            prm_doc_problems.Value = DBNull.Value;
        }

        if (doc_prescription != null)
        {
            prm_doc_prescription.Value = doc_prescription;
        }
        else
        {
            prm_doc_prescription.Value = DBNull.Value;
        }

        if (doc_diagnostictest != null)
        {
            prm_doc_diagnostictest.Value = doc_diagnostictest;
        }
        else
        {
            prm_doc_diagnostictest.Value = DBNull.Value;
        }

        if (doc_lab != null)
        {
            prm_doc_lab.Value = doc_lab;
        }
        else
        {
            prm_doc_lab.Value = DBNull.Value;
        }

        if (doc_procedures != null)
        {
            prm_doc_procedures.Value = doc_procedures;
        }
        else
        {
            prm_doc_procedures.Value = DBNull.Value;
        }

        if (doc_immunization != null)
        {
            prm_doc_immunization.Value = doc_immunization;
        }
        else
        {
            prm_doc_immunization.Value = DBNull.Value;
        }

        if (doc_pat_educations != null)
        {
            prm_doc_pat_educations.Value = doc_pat_educations;
        }
        else
        {
            prm_doc_pat_educations.Value = DBNull.Value;
        }

        if (doc_respond != null)
        {
            prm_doc_respond.Value = doc_respond;
        }
        else
        {
            prm_doc_respond.Value = DBNull.Value;
        }

        if (doc_refer != null)
        {
            prm_doc_refer.Value = doc_refer;
        }
        else
        {
            prm_doc_refer.Value = DBNull.Value;
        }

        prm_examined_by_provider.Value = examined_by_provider;

        if (doc_radiology != null)
        {
	        prm_doc_radiology.Value = doc_radiology; 
        }
        else
        {
            prm_doc_radiology.Value = DBNull.Value;
        }

        if (doc_performance_measurement != null)
        {
            prm_doc_performance_measurement.Value = doc_performance_measurement;
        }
        else
        {
            prm_doc_performance_measurement.Value = DBNull.Value;
        }

        if (doc_emerging_tech_sv != null)
        {
            prm_doc_emerging_tech_sv.Value = doc_emerging_tech_sv; 
        }
        else
        {
            prm_doc_emerging_tech_sv.Value = DBNull.Value;
        }

        if (doc_other_text != null)
        {
            prm_doc_other_text.Value = doc_other_text; 
        }
        else
        {
            prm_doc_other_text.Value = DBNull.Value;
        }

        if (doc_followup != null)
        {
            prm_doc_followup.Value = doc_followup; 
        }
        else
        {
            prm_doc_followup.Value = DBNull.Value;
        }

        if (doc_other != null)
        {
            prm_doc_other.Value = doc_other; 
        }
        else
        {
            prm_doc_other.Value = DBNull.Value;
        }

        //Add the parameters in...
        cmd.Parameters.Add(prm_owner_id);
        cmd.Parameters.Add(prm_patient_key);
        cmd.Parameters.Add(prm_date_added);
        cmd.Parameters.Add(prm_code_type);
        cmd.Parameters.Add(prm_doctor_id);
        cmd.Parameters.Add(prm_nurse_id);
        cmd.Parameters.Add(prm_recorded_vitalsigns_id);
        cmd.Parameters.Add(prm_alert_id);
        cmd.Parameters.Add(prm_subjective);
        cmd.Parameters.Add(prm_objective);
        cmd.Parameters.Add(prm_assessment);
        cmd.Parameters.Add(prm_notes);

        cmd.Parameters.Add(prm_doc_problems);
        cmd.Parameters.Add(prm_doc_prescription);
        cmd.Parameters.Add(prm_doc_diagnostictest);
        cmd.Parameters.Add(prm_doc_lab);
        cmd.Parameters.Add(prm_doc_procedures);
        cmd.Parameters.Add(prm_doc_immunization);
        cmd.Parameters.Add(prm_doc_pat_educations);
        cmd.Parameters.Add(prm_doc_respond);
        cmd.Parameters.Add(prm_doc_refer);

        cmd.Parameters.Add(prm_umr_plan_id);
        cmd.Parameters.Add(prmIdentity);

        cmd.Parameters.Add(prm_examined_by_provider);
        cmd.Parameters.Add(prm_doc_radiology);
        cmd.Parameters.Add(prm_doc_performance_measurement);
        cmd.Parameters.Add(prm_doc_emerging_tech_sv);
        cmd.Parameters.Add(prm_doc_other_text);
        cmd.Parameters.Add(prm_doc_followup);
        cmd.Parameters.Add(prm_doc_other);

        identiy = 0;
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            identiy = (long)prmIdentity.Value;
            cmd.Connection.Close();
        }
        catch (SqlException Ex)
        {
            LogError(Ex);

            ErrorMessage = "An Unspecified Error Occurred[Error Code : " + Ex.ToString() + "]. Please contact your administrator with the error code to resolve this issue";

            return -1;
        }
        catch (Exception Ex) { LogError(Ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return 1;
    }
    #endregion

    #region AddPatientDiagonses
    public int AddPatientDiagonses(long patient_key,
        string code_type,
        int code_version,
        string medcode,
        DateTime codedate,
        char hospitalization,
        string institution_code,
        string doctor_id,
        int visibility,
        out string ErrorMessage)
    {
        #region SP FILEDS
        /*
		@patient_key bigint,
        @code_type nvarchar,
        @code_version numeric,
        @medcode nvarchar,
        @codedate datetime,
        @hospitalization nchar,
        @institution_code nvarchar,
        @doctor_id nvarchar,
        @visibility numeric
         */
        #endregion

        string spName = "SP_PATIENT_DIAGNOSES_ADD";
        ErrorMessage = "";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prm_patient_key = new SqlParameter("@patient_key", SqlDbType.BigInt);
        SqlParameter prm_code_type = new SqlParameter("@code_type", SqlDbType.NVarChar);
        SqlParameter prm_code_version = new SqlParameter("@code_version", SqlDbType.Int);
        SqlParameter prm_medcode = new SqlParameter("@medcode", SqlDbType.NVarChar);
        SqlParameter prm_codedate = new SqlParameter("@codedate", SqlDbType.DateTime);
        SqlParameter prm_hospitalization = new SqlParameter("@hospitalization", SqlDbType.NChar);
        SqlParameter prm_institution_code = new SqlParameter("@institution_code", SqlDbType.NVarChar);
        SqlParameter prm_doctor_id = new SqlParameter("@doctor_id", SqlDbType.NVarChar);
        SqlParameter prm_visibility = new SqlParameter("@visibility", SqlDbType.Int);


        //Set values in the parameters


        prm_patient_key.Value = patient_key;
        prm_code_type.Value = code_type.Trim();
        prm_code_version.Value = code_version;
        prm_medcode.Value = medcode.Trim();
        prm_codedate.Value = codedate;
        prm_hospitalization.Value = hospitalization;
        prm_visibility.Value = visibility;

        if (institution_code!=null)
        {
            prm_institution_code.Value = institution_code;
        }
        else
        {
            prm_institution_code.Value = DBNull.Value;
        }

        if (doctor_id != null)
        {
            prm_doctor_id.Value = doctor_id;
        }
        else
        {
            prm_doctor_id.Value = DBNull.Value;
        }


        //Add the parameters in...
        cmd.Parameters.Add(prm_patient_key);
        cmd.Parameters.Add(prm_code_type);
        cmd.Parameters.Add(prm_code_version);
        cmd.Parameters.Add(prm_medcode);
        cmd.Parameters.Add(prm_codedate);
        cmd.Parameters.Add(prm_hospitalization);
        cmd.Parameters.Add(prm_institution_code);
        cmd.Parameters.Add(prm_doctor_id);
        cmd.Parameters.Add(prm_visibility);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (SqlException Ex)
        {
            LogError(Ex);

            ErrorMessage = "An Unspecified Error Occurred[Error Code : " + Ex.ToString() + "]. Please contact your administrator with the error code to resolve this issue";

            return -1;
        }
        catch (Exception Ex) { LogError(Ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return 1;
    }
    #endregion

    #region AddSoapsAddress
    public int AddSoapsAddress(long soaps_id,
        string emailaddress,
        string smsnumber,
        string doctor_id,
        string nurse_id,
        string institution_id,
        out long identiy,
        out string ErrorMessage)
    {
        #region SP FILEDS
        /*
       @soaps_id bigint,
       @emailaddress nvarchar(50),
       @smsnumber nvarchar(50),
       @doctor_id nvarchar(15),
       @nurse_id nvarchar(15),
       @institution_id nvarchar(15),
   @Identity bigint OUT
         */
        #endregion

        string spName = "SP_ADD_SOAP_ADDRESS";
        ErrorMessage = "";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prm_soaps_id = new SqlParameter("@soaps_id", SqlDbType.BigInt);
        SqlParameter prm_emailaddress = new SqlParameter("@emailaddress", SqlDbType.NVarChar);
        SqlParameter prm_smsnumber = new SqlParameter("@smsnumber", SqlDbType.NVarChar);
        SqlParameter prm_doctor_id = new SqlParameter("@doctor_id", SqlDbType.NVarChar);
        SqlParameter prm_nurse_id = new SqlParameter("@nurse_id", SqlDbType.NVarChar);
        SqlParameter prm_institution_id = new SqlParameter("@institution_id", SqlDbType.NVarChar);

        SqlParameter prmIdentity = new SqlParameter("@Identity", SqlDbType.BigInt);
        prmIdentity.Direction = ParameterDirection.Output;

        //Set values in the parameters
        prm_soaps_id.Value = soaps_id;
        if (emailaddress != null)
        {
            prm_emailaddress.Value = emailaddress;
        }
        else
        {
            prm_emailaddress.Value = DBNull.Value;
        }

        if (smsnumber != null)
        {
            prm_smsnumber.Value = smsnumber;
        }
        else
        {
            prm_smsnumber.Value = DBNull.Value;
        }

        if (doctor_id != null)
        {
            prm_doctor_id.Value = doctor_id;
        }
        else
        {
            prm_doctor_id.Value = DBNull.Value;
        }

        if (nurse_id != null)
        {
            prm_nurse_id.Value = nurse_id;
        }
        else
        {
            prm_nurse_id.Value = DBNull.Value;
        }

        if (institution_id != null)
        {
            prm_institution_id.Value = institution_id;
        }
        else
        {
            prm_institution_id.Value = DBNull.Value;
        }

        //Add the parameters in...
        cmd.Parameters.Add(prm_soaps_id);
        cmd.Parameters.Add(prm_emailaddress);
        cmd.Parameters.Add(prm_smsnumber);
        cmd.Parameters.Add(prm_doctor_id);
        cmd.Parameters.Add(prm_nurse_id);
        cmd.Parameters.Add(prm_institution_id);
        cmd.Parameters.Add(prmIdentity);

        identiy = 0;
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            identiy = (long)prmIdentity.Value;
            cmd.Connection.Close();
        }
        catch (SqlException Ex)
        {
            LogError(Ex);

            ErrorMessage = "An Unspecified Error Occurred[Error Code : " + Ex.ToString() + "]. Please contact your administrator with the error code to resolve this issue";

            return -1;
        }
        catch (Exception Ex) { LogError(Ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return 1;
    }
    #endregion

    #region AddAlertEmail
    public int AddAlertEmail(long ALERT_ID,
        string ALERT_EMAIL_BODY,
        string EMAIL_RECIEPENTS,
        out string ErrorMessage)
    {
        #region SP FILEDS
        /*
           @ALERT_ID bigint,
           @ALERT_EMAIL_BODY nvarchar(max),
           @EMAIL_RECIEPENTS nvarchar(2000) 
        */
        #endregion

        string spName = "SP_ADD_PATIENT_ALERT_EMAIL";
        ErrorMessage = "";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmALERT_ID = new SqlParameter("@ALERT_ID", SqlDbType.BigInt);
        SqlParameter prmALERT_EMAIL_BODY = new SqlParameter("@ALERT_EMAIL_BODY", SqlDbType.NVarChar);
        SqlParameter prmEMAIL_RECIEPENTS = new SqlParameter("@EMAIL_RECIEPENTS", SqlDbType.NVarChar);

        //Set values in the parameters
        prmALERT_ID.Value = ALERT_ID;
        prmALERT_EMAIL_BODY.Value = ALERT_EMAIL_BODY;
        prmEMAIL_RECIEPENTS.Value = EMAIL_RECIEPENTS;


        //Add the parameters in...
        cmd.Parameters.Add(prmALERT_ID);
        cmd.Parameters.Add(prmALERT_EMAIL_BODY);
        cmd.Parameters.Add(prmEMAIL_RECIEPENTS);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (SqlException Ex)
        {
            LogError(Ex);

            ErrorMessage = "An Unspecified Error Occurred[Error Code : " + Ex.ToString() + "]. Please contact your administrator with the error code to resolve this issue";

            return -1;
        }
        catch (Exception Ex) { LogError(Ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return 1;
    }
    #endregion

    public DataSet GetPatientVitalSigns(long patientKey)
    {
        string spName = "SP_GET_VITALSIGNS";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmpatientKey = new SqlParameter("p_PatientKey", SqlDbType.NVarChar);

        //Set values in the parameters
        prmpatientKey.Value = patientKey;


        //Add the parameters in...
        cmd.Parameters.Add(prmpatientKey);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        // //
    }

    public DataSet GetPatientVitalSignsByID(int id)
    {
        //@id int
        string spName = "SP_GET_VITALSIGNS_BY_ID";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmp_id = new SqlParameter("@id", SqlDbType.Int);

        //Set values in the parameters
        prmp_id.Value = id;


        //Add the parameters in...
        cmd.Parameters.Add(prmp_id);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        // //
    }

    public DataSet GetPatientSoapsByAlertAndNurseID(long alert_id, string nurse_id)
    {
        //@alert_id bigint,
        //@nurse_id nvarchar(15)

        string spName = "SP_GET_UMR_PATIENT_SOAPS_BY_ALERT_AND_NURSE_ID";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prm_alert_id = new SqlParameter("@alert_id", SqlDbType.BigInt);
        SqlParameter prm_nurse_id = new SqlParameter("@nurse_id", SqlDbType.NVarChar);

        //Set values in the parameters
        prm_alert_id.Value = alert_id;
        prm_nurse_id.Value = nurse_id;


        //Add the parameters in...
        cmd.Parameters.Add(prm_alert_id);
        cmd.Parameters.Add(prm_nurse_id);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        // //
    }

    public DataSet GetPatientNurseSoapsByAlertID(long alert_id)
    {
        //@alert_id bigint,
        //@nurse_id nvarchar(15)

        string spName = "SP_GET_UMR_NURSE_PATIENT_SOAPS_BY_ALERT_ID";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prm_alert_id = new SqlParameter("@alert_id", SqlDbType.BigInt);

        //Set values in the parameters
        prm_alert_id.Value = alert_id;


        //Add the parameters in...
        cmd.Parameters.Add(prm_alert_id);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        // //
    }

    public DataSet GetPatientSoapsBySOAPID(long soaps_id)
    {
        //@alert_id bigint,
        //@nurse_id nvarchar(15)

        string spName = "SP_GET_UMR_DOC_PATIENT_SOAPS_BY_SOAP_ID";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prm_alert_id = new SqlParameter("@SOAPS_ID", SqlDbType.BigInt);

        //Set values in the parameters
        prm_alert_id.Value = soaps_id;


        //Add the parameters in...
        cmd.Parameters.Add(prm_alert_id);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        // //
    }
    public DataSet GetPatientPatientSoapsAddressBySoapID(long soaps_id)
    {
        //@soaps_id bigint

        string spName = "SP_GET_UMR_PATIENT_SOAPS_ADDRESS_BY_SOAPS_ID";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prm_soaps_id = new SqlParameter("@soaps_id", SqlDbType.NVarChar);

        //Set values in the parameters
        prm_soaps_id.Value = soaps_id;


        //Add the parameters in...
        cmd.Parameters.Add(prm_soaps_id);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        // //
    }
    
    public int GetPatientGetActiveAlertCount(long PATIENT_KEY, byte ALERT_STATUS)
    {
        //@PATIENT_KEY AS BIGINT,
        //@ALERT_STATUS AS TINYINT

        int count = 0;
        string spName = "SP_GET_ACTIVE_ALERT_COUNT_FOR_PATIENT";


        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prm_PATIENT_KEY = new SqlParameter("@PATIENT_KEY", SqlDbType.BigInt);
        SqlParameter prm_ALERT_STATUS = new SqlParameter("@ALERT_STATUS", SqlDbType.TinyInt);

        //Set values in the parameters
        prm_PATIENT_KEY.Value = PATIENT_KEY;
        prm_ALERT_STATUS.Value = ALERT_STATUS;

        //Add the parameters in...
        cmd.Parameters.Add(prm_PATIENT_KEY);
        cmd.Parameters.Add(prm_ALERT_STATUS);
        try
        {
            cmd.Connection.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
        }
        catch (Exception ex) { LogError(ex); return -1; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return count;
    }

    public DataSet GetPatientGetActiveAlert(long PATIENT_KEY, byte ALERT_STATUS)
    {
        //@PATIENT_KEY AS BIGINT,
        //@ALERT_STATUS AS TINYINT

        string spName = "SP_GET_ACTIVE_ALERT_FOR_PATIENT";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prm_PATIENT_KEY = new SqlParameter("@PATIENT_KEY", SqlDbType.BigInt);
        SqlParameter prm_ALERT_STATUS = new SqlParameter("@ALERT_STATUS", SqlDbType.TinyInt);

        //Set values in the parameters
        prm_PATIENT_KEY.Value = PATIENT_KEY;
        prm_ALERT_STATUS.Value = ALERT_STATUS;


        //Add the parameters in...
        cmd.Parameters.Add(prm_PATIENT_KEY);
        cmd.Parameters.Add(prm_ALERT_STATUS);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        // //
    }

    public DataSet GetPlans()
    {
        string spName = "SP_GET_PLANS";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        
        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        // //
    }


    public DataSet GetPatientDOCSoapsByAlertID(long alert_id)
    {
        //@alert_id bigint,
        //@nurse_id nvarchar(15)

        string spName = "SP_GET_UMR_DOC_PATIENT_SOAPS_BY_ALERT_ID";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prm_alert_id = new SqlParameter("@alert_id", SqlDbType.BigInt);

        //Set values in the parameters
        prm_alert_id.Value = alert_id;


        //Add the parameters in...
        cmd.Parameters.Add(prm_alert_id);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        // //
    }

    public int GetPatientGetActiveDOCAlertCount(long PATIENT_KEY, byte ALERT_STATUS)
    {
        //@PATIENT_KEY AS BIGINT,
        //@ALERT_STATUS AS TINYINT

        int count = 0;
        string spName = "SP_GET_ACTIVE_ALERT_WITH_DOC_SOAP_COUNT_FOR_PATIENT";


        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prm_PATIENT_KEY = new SqlParameter("@PATIENT_KEY", SqlDbType.BigInt);
        SqlParameter prm_ALERT_STATUS = new SqlParameter("@ALERT_STATUS", SqlDbType.TinyInt);

        //Set values in the parameters
        prm_PATIENT_KEY.Value = PATIENT_KEY;
        prm_ALERT_STATUS.Value = ALERT_STATUS;

        //Add the parameters in...
        cmd.Parameters.Add(prm_PATIENT_KEY);
        cmd.Parameters.Add(prm_ALERT_STATUS);
        try
        {
            cmd.Connection.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
        }
        catch (Exception ex) { LogError(ex); return -1; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return count;
    }

    public DataSet GetPatientGetActiveDOCAlert(long PATIENT_KEY, byte ALERT_STATUS)
    {
        //@PATIENT_KEY AS BIGINT,
        //@ALERT_STATUS AS TINYINT

        string spName = "SP_GET_ACTIVE_ALERT_WITH_DOC_SOAP_FOR_PATIENT";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prm_PATIENT_KEY = new SqlParameter("@PATIENT_KEY", SqlDbType.BigInt);
        SqlParameter prm_ALERT_STATUS = new SqlParameter("@ALERT_STATUS", SqlDbType.TinyInt);

        //Set values in the parameters
        prm_PATIENT_KEY.Value = PATIENT_KEY;
        prm_ALERT_STATUS.Value = ALERT_STATUS;


        //Add the parameters in...
        cmd.Parameters.Add(prm_PATIENT_KEY);
        cmd.Parameters.Add(prm_ALERT_STATUS);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        // //
    }

    #region UpdateAlerts
    public int UpdateAlertWithDoctorResponse(long alert_id,
        DateTime? doctor_soap_time_stamp,
        bool? has_doctor_soap,
        byte alert_status,
        bool is_active,
        out string ErrorMessage)
    {
        #region SP FILEDS
           //@alert_id bigint,
           //@doctor_soap_time_stamp datetime,
           //@has_doctor_soap bit,
           //@alert_status tinyint,
           //@is_active bit
        #endregion

        string spName = "SP_UPDATE_ALERT_WITH_DOCTOR_RESPONSE";
        ErrorMessage = "";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmAlertID = new SqlParameter("@alert_id", SqlDbType.BigInt);
        SqlParameter prmDoctorSoapTimeStamp = new SqlParameter("@doctor_soap_time_stamp", SqlDbType.DateTime);
        SqlParameter prmHasDoctorSoap = new SqlParameter("@has_doctor_soap", SqlDbType.Bit);
        SqlParameter prmAlertStatus = new SqlParameter("@alert_status", SqlDbType.TinyInt);
        SqlParameter prmIsActive = new SqlParameter("@is_active", SqlDbType.Bit);

        //Set values in the parameters
        prmAlertID.Value = alert_id;
        if (doctor_soap_time_stamp != null)
        {
            prmDoctorSoapTimeStamp.Value = doctor_soap_time_stamp;
        }
        else
        {
            prmDoctorSoapTimeStamp.Value = DBNull.Value;
        }

        if (has_doctor_soap != null)
        {
            prmHasDoctorSoap.Value = has_doctor_soap;
        }
        else
        {
            prmHasDoctorSoap.Value = DBNull.Value;
        }

        prmAlertStatus.Value = alert_status;
        prmIsActive.Value = is_active;

        //Add the parameters in...
        cmd.Parameters.Add(prmAlertID);
        cmd.Parameters.Add(prmDoctorSoapTimeStamp);
        cmd.Parameters.Add(prmHasDoctorSoap);
        cmd.Parameters.Add(prmAlertStatus);
        cmd.Parameters.Add(prmIsActive);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (SqlException Ex)
        {
            LogError(Ex);

            ErrorMessage = "An Unspecified Error Occurred[Error Code : " + Ex.ToString() + "]. Please contact your administrator with the error code to resolve this issue";

            return -1;
        }
        catch (Exception Ex) { LogError(Ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return 1;
    }
    #endregion

    public int AddReportSMS(string reportHtml)
    {
        string spName = "SP_REPORT_SMS_ADD";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter prmReportHtml = new SqlParameter("p_ReportHtml", SqlDbType.NVarChar);

        //Set values in the parameters
        prmReportHtml.Value = reportHtml;

        //Add the parameters in...
        cmd.Parameters.Add(prmReportHtml);


        try
        {
            cmd.Connection.Open();
            object id = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return Convert.ToInt32(id);
        }
        catch (SqlException Ex)
        {
            LogError(Ex);

            return -1;
        }
        catch (Exception Ex) { LogError(Ex); throw; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        return 0;
    }

    //

    public DataSet GET_UMR_PROVIDER_BY_NAME(string name_part)
    {
        //@name_part

        string spName = "SP_GET_UMR_PROVIDER_BY_NAME";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prm_name_part = new SqlParameter("@name_part", SqlDbType.NVarChar);

        //Set values in the parameters
        prm_name_part.Value = name_part;


        //Add the parameters in...
        cmd.Parameters.Add(prm_name_part);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        // //
    }


    public DataSet GET_NURSE_BY_ID(string nurse_id)
    {
        //@NURSE_ID

        string spName = "SP_ALERT_GET_NURSE_BY_ID";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prm_name_part = new SqlParameter("@NURSE_ID", SqlDbType.NVarChar);

        //Set values in the parameters
        prm_name_part.Value = nurse_id;


        //Add the parameters in...
        cmd.Parameters.Add(prm_name_part);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
        // //
    }

    public DataSet GetSMSReportHTMLById(int reportId)
    {
        string spName = "SP_REPORT_GET";

        //Create the SQL Command
        SqlCommand cmd = new SqlCommand(spName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        //Create the SQL Parameters
        SqlParameter prmid = new SqlParameter("p_id", SqlDbType.Int);

        //Set values in the parameters
        prmid.Value = reportId;


        //Add the parameters in...
        cmd.Parameters.Add(prmid);

        //Create the DataSet
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex) { LogError(ex); return null; }
        finally
        {
            if ((cmd.Connection != null) && (cmd.Connection.State != ConnectionState.Closed))
            {
                cmd.Connection.Close();
            }
        }
    }

    #region GET DIFFERENT CODES
    public DataSet GetAllDiagnosticTest(string medcode)
    {
        string spName = "SP_ALERT_GET_ALL_DIAGNOSTIC_TESTS";
        string parameterName = "@CHECK_MEDCODE";
        return GetCodeData(medcode, spName, parameterName);
    }
    public DataSet GetRadiology(string medcode)
    {
        string spName = "SP_ALERT_GET_RADIOLOGY";
        string parameterName = "CHECK_MEDCODE";
        return GetCodeData(medcode, spName, parameterName);
    }
    public DataSet GetPathologyLaboratory(string medcode)
    {
        string spName = "SP_ALERT_GET_PATHOLOGY_LABORATORY";
        string parameterName = "CHECK_MEDCODE";
        return GetCodeData(medcode, spName, parameterName);
    }
    public DataSet GetProceduresOthers(string medcode)
    {
        string spName = "SP_ALERT_GET_PROCEDURES_OTHERS";
        string parameterName = "CHECK_MEDCODE";
        return GetCodeData(medcode, spName, parameterName);
    }
    public DataSet GetPerformanceMeasurements(string medcode)
    {
        string spName = "SP_ALERT_GET_PERFORMANCE_MEASUREMENTS";
        string parameterName = "CHECK_MEDCODE";
        return GetCodeData(medcode, spName, parameterName);
    }
    public DataSet GetEmergingTechServices(string medcode)
    {
        string spName = "SP_ALERT_GET_EMERGING_TEC_SERVICES";
        string parameterName = "CHECK_MEDCODE";
        return GetCodeData(medcode, spName, parameterName);
    }

    private DataSet GetCodeData(string medcode, string spName, string parameterName)
    {
        SqlCommand command = new SqlCommand(spName, con);
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter checkParameter = new SqlParameter(parameterName, SqlDbType.VarChar, 10);
        checkParameter.Value = medcode;
        command.Parameters.Add(checkParameter);

        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataSet ds = new DataSet();
        try
        {
            command.Connection.Open();
            adapter.Fill(ds);
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
        finally
        {
            if ((command.Connection != null) && (command.Connection.State != ConnectionState.Closed))
            {
                command.Connection.Close();
            }
            command.Dispose();
        }
        return ds;
    }
    #endregion //end GET DIFFERENT CODES
    #endregion
}
