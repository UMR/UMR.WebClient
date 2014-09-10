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

using System.Drawing;
using System.Collections;
using System.Text;

/// <summary>
/// This layer simply communicates with DAL for data. 
/// This may look redundant for now, but if we opt for a different caching mechanism, 
/// this layer will play a pivotal role in that...
/// </summary>

public class PatientManager
{


    //private static DataAccess DAL = new DataAccess();
    public PatientManager() { }

    public static DateTime dateRangeStart = DateTime.Now;
    public static DateTime dateRangeEnd
    {
        get
        {
            DateTime _dateRangeEnd = DateTime.Now;
            if (HttpContext.Current.Session["dateRangeEnd"] != null)
            {
                _dateRangeEnd = DateTime.Parse(HttpContext.Current.Session["dateRangeEnd"].ToString().Trim());
            }
            return _dateRangeEnd;
        }
        set
        {
            HttpContext.Current.Session["dateRangeEnd"] = value;
        }
    }
    public static bool DateRangeApplied
    {
        get
        {
            bool dateRangeApplied = false;
            if (HttpContext.Current.Session["DateRangeApplied"] != null)
            {
                dateRangeApplied = Boolean.Parse(HttpContext.Current.Session["DateRangeApplied"].ToString().Trim());
            }
            return dateRangeApplied;
        }
        set
        {
            HttpContext.Current.Session["DateRangeApplied"] = value;
        }
    }
    public static bool ValidateUser(string UserName, string Password, out string ErrorMessage)
    {
        DataAccess DAL = new DataAccess();
        return DAL.ValidateUser(UserName, Password, out ErrorMessage);
    }

    public static int GetPatientCount(string ID, string ModifierID, string FName, string LName, DateTime DateOfBirth)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientCount(ID, ModifierID, FName, LName, DateOfBirth);
    }

    //***********
    public static DataSet GetPatientList(string ID, string ModifierID, string FName, string LName, string DateOfBirth)//, int startRowIndex, int maximumRows)
    {
        if (FName != null)
        {
            FName = FName.Replace("'", "''");
        }
        if (LName != null)
        {
            LName = LName.Replace("'", "''");
        }
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientList(ID, ModifierID, FName, LName, DateOfBirth);
    }
    public static DataTable GetPatientList(string ID, string ModifierID, string FName, string LName, DateTime DateOfBirth, int currentPageIndex, int pageSize, string orderBy)
    {
        if (FName != null)
        {
            FName = FName.Replace("'", "''");
        }
        if (LName != null)
        {
            LName = LName.Replace("'", "''");
        }
        DataAccess DAL = new DataAccess();
        DataSet ds = DAL.GetPatientList(ID, ModifierID, FName, LName, DateOfBirth, currentPageIndex, pageSize, orderBy);
        return ds.Tables.Count != 0 ? ds.Tables[0] : new DataTable();
    }
    //***********
    //public static List<Patients> GetPatientList(long patientKey, string FName, string LName)//, string DateOfBirth)//, int startRowIndex, int maximumRows)
    //{
    //    return DAL.GetPatientList(ID, ModifierID, FName, LName);//, DateOfBirth);
    //}

    public static DataSet GetPatientDemographics(long patientKey, string UserId)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientDemographics(patientKey, UserId);
        //if (HttpContext.Current.Session["dsDemographics"] != null)
        //{
        //    return (DataSet)(HttpContext.Current.Session["dsDemographics"]);
        //}
        //else
        //{
        //    DataSet ds = DAL.GetPatientDemographics(ID, ModifierID, UserId);
        //    //Add the Dataset in the session
        //    if (HttpContext.Current.Session["dsDemoGraphics"] != null)
        //    {
        //        //First Clear the current records
        //        HttpContext.Current.Session.Remove("dsDemoGraphics");
        //        HttpContext.Current.Session.Add("dsDemoGraphics", ds);
        //    }
        //    else
        //    {
        //        HttpContext.Current.Session.Add("dsDemoGraphics", ds);
        //    }
        //    return ds;
        //}
    }

    //public static DataSet GetEmergencyContact()
    //{
    //    return DAL.GetEmergencyContact();
    //}

    public static DataSet GetEmergencyContact(long patientKey, string UserId)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetEmergencyContact(patientKey, UserId);
    }

    //public static DataSet GetPrincipalHealthCareInfo()
    //{
    //    return DAL.GetPrincipalHealthCareInfo();
    //}

    public static DataSet GetPrincipalHealthCareInfo(long patientKey, string UserId)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPrincipalHealthCareInfo(patientKey, UserId);
    }
    public static DataSet GetRemarkableDisciplineList(long patientKey)
    {
        return GetRemarkableDisciplineList(patientKey, null);
    }

    public static DataSet GetRemarkableDisciplineList(long patientKey, string UserID)
    {
        DataAccess DAL = new DataAccess();
        ////**************************************************
        //DataSet dsRDList;
        //string cacheKey = ID + ModifierID + "RDList";
        //if (HttpContext.Current.Session[cacheKey] != null)
        //{
        //    dsRDList = (DataSet)HttpContext.Current.Session[cacheKey];
        //}
        //else
        //{
        //    dsRDList = DAL.GetRemarkableDisciplineList(ID, ModifierID, UserID);
        //    if ((dsRDList != null) && (dsRDList.Tables[0].Rows.Count > 0))
        //    {
        //        HttpContext.Current.Session.Add(cacheKey, dsRDList);
        //    }
        //}
        //return dsRDList;
        ////**************************************************
        DataSet ds = null;
        if (DateRangeApplied)
        {
            ds = DAL.GetRemarkableDisciplineList(patientKey, UserID, dateRangeStart, dateRangeEnd);
        }
        else
        {
            ds = DAL.GetRemarkableDisciplineList(patientKey, UserID);
        }

        //Set Last Aceess Information in to Session
        if (ds != null && UserID != null)
        {
            if (HttpContext.Current.Session["LAST_ACCESS_INFO"] != null)
            {
                System.Data.DataTable dtLastAccessInfo = HttpContext.Current.Session["LAST_ACCESS_INFO"] as System.Data.DataTable;
                DataRow newRow = dtLastAccessInfo.NewRow();
                newRow[0] = ds.Tables[1].Rows[0][0];
                newRow[1] = ds.Tables[1].Rows[0][1];
                newRow[2] = ds.Tables[1].Rows[0][2];
                dtLastAccessInfo.Rows.Add(newRow);
                HttpContext.Current.Session["LAST_ACCESS_INFO"] = dtLastAccessInfo;
            }
            else
            {
                HttpContext.Current.Session["LAST_ACCESS_INFO"] = ds.Tables[1];
            }
        }
        /////////

        return ds;
    }
    public static DataTable GetSortedRDs(string disCodes)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetSortedRDs(disCodes);
    }

    public static DataSet GetRemarkableDisciplineListForMedCode(string medCode, string excludedDisCode)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetRemarkableDisciplineListForMedCode(medCode, excludedDisCode);
    }

    public static DataTable GetDistinctSpecificServiceDate(long patientKey, string codeType)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetDistinctSpecificServiceDate(patientKey, codeType).Tables[0];
    }

    public static DataTable GetSpecificServiceDetails(long patientKey, string codeType, DateTime serviceDate)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetSpecificServiceDetails(patientKey, codeType, serviceDate).Tables[0];
    }

    //******
    public static System.Data.SqlClient.SqlDataReader GetRDDiagnosisDR(long patientKey, string codeType, string medCode, decimal codeVersion, decimal disCode)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetRDDiagnosisDR(patientKey, codeType, medCode, codeVersion, disCode);
    }

    //********
    //public static DataSet GetRDDiagnosis(long patientKey, string codeType, string medCode, decimal codeVersion, decimal disCode)
    //{
    //    return DAL.GetRDDiagnosis(id, modifierID, codeType, medCode, codeVersion, disCode);
    //}
    public static DataSet GetRDDiagnosis(long patientKey, decimal disCode)
    {
        DataAccess DAL = new DataAccess();
        if (DateRangeApplied)
        {
            return DAL.GetRDDiagnosis(patientKey, disCode, dateRangeStart, dateRangeEnd);
        }
        else
        {
            return DAL.GetRDDiagnosis(patientKey, disCode);
        }
    }

    //public static DataSet GetRDDiagnosisDetail(long patientKey, string codeType, string medCode, decimal codeVersion)
    //{
    //    return DAL.GetRDDiagnosisDetail(id, modifierID, codeType, medCode, codeVersion);
    //}
    public static DataSet GetRDDiagnosisDetail(long patientKey, string medCode)
    {
        DataAccess DAL = new DataAccess();
        DataSet ds = DAL.GetRDDiagnosisDetail(patientKey, medCode);

        if (DateRangeApplied)
        {
            DataTable tmpTable = ds.Tables[0].Copy();
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(tmpTable.Rows[i]["DateOfService"].ToString().Trim());
                if (date > dateRangeStart || date < dateRangeEnd)
                {
                    ds.Tables[0].Rows[i].Delete();
                }
            }
            ds.AcceptChanges();
        }

        return ds;
    }

    public static DataSet GetRDMedication(long patientKey)
    {
        DataAccess DAL = new DataAccess();
        if (DateRangeApplied)
        {
            return DAL.GetRDMedication(patientKey, dateRangeStart, dateRangeEnd);
        }
        else
        {
            return DAL.GetRDMedication(patientKey);
        }
    }

    public static DataSet GetRDMedicationDetail(long patientKey, string ndcCode)
    {
        DataAccess DAL = new DataAccess();
        DataSet ds = DAL.GetRDMedicationDetail(patientKey, ndcCode);
        if (DateRangeApplied)
        {
            DataTable tmpTable = ds.Tables[0].Copy();
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(tmpTable.Rows[i]["DateOfService"].ToString().Trim());
                if (date > dateRangeStart || date < dateRangeEnd)
                {
                    ds.Tables[0].Rows[i].Delete();
                }
            }
            ds.AcceptChanges();
        }

        return ds;
    }

    public static DataSet GetProviderInfo(string id)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetProviderInfo(id);
    }

    public static DataSet GetMPIInfo(long patientKey)
    {
        DataAccess DAL = new DataAccess();
        DataSet ds = DAL.GetMPIInfo(patientKey);

        if (DateRangeApplied)
        {
            DataTable tmpTable = ds.Tables[0].Copy();
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(tmpTable.Rows[i]["DateOfService"].ToString().Trim() + " " + tmpTable.Rows[i]["ServiceTime"].ToString().Trim());
                if (date > dateRangeStart || date < dateRangeEnd)
                {
                    ds.Tables[0].Rows[i].Delete();
                }
            }
            ds.AcceptChanges();
        }

        return ds;
    }
    public static DataSet GetMPIInfoSpecific(long patientKey, string doctorID, string serviceDate)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetMPIInfoSpecific(patientKey, doctorID, serviceDate);
    }
    public static DataSet GetLRPMInfo(long patientKey)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetLRPMInfo(patientKey);
    }

    public static DataSet GetLRAInfo(object patientKey)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetLRAInfo(patientKey);
    }

    public static DataSet GetMultipleProviderInfo(long patientKey)
    {
        DataAccess DAL = new DataAccess();
        if (DateRangeApplied)
        {
            return DAL.GetMultipleProviderInfo(patientKey, dateRangeStart, dateRangeEnd);
        }
        else
        {
            return DAL.GetMultipleProviderInfo(patientKey);
        }
    }

    public static DataSet GetLastDates(long patientKey)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetLastDates(patientKey);
    }

    public static List<string> GetAllRD()
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetAllRD();
    }

    internal static void SaveUserSettings(string UserID, string UserSettings)
    {
        DataAccess DAL = new DataAccess();
        //*************************************************
        DAL.SaveUserSettings(UserID, UserSettings);
        DataTable userSettingsTable = DAL.GetUserSettings(UserID);
        string cacheKey = UserID + "UserSettings";
        if (userSettingsTable != null)
        {
            if (userSettingsTable.Rows.Count > 0)
            {
                if (HttpContext.Current.Session[cacheKey] != null)
                {
                    HttpContext.Current.Session.Remove(cacheKey);
                }
                HttpContext.Current.Session.Add(cacheKey, userSettingsTable.Rows[0][0].ToString());
            }
        }
        //*************************************************
        //DAL.SaveUserSettings(UserID, UserSettings);
        //DataTable userSettingsTable = DAL.GetUserSettings(UserID);
        //if (userSettingsTable != null)
        //{
        //    if (userSettingsTable.Rows.Count > 0)
        //    {
        //        if (HttpContext.Current.Session["UserSettings"] != null)
        //        {
        //            HttpContext.Current.Session.Remove("UserSettings");
        //        }
        //        HttpContext.Current.Session.Add("UserSettings", userSettingsTable.Rows[0][0].ToString());
        //    }
        //}
    }

    public static bool IsFirstVisit(string UserID, out DataTable UserSettings)
    {
        DataAccess DAL = new DataAccess();
        //*********************************************************
        UserSettings = DAL.GetUserSettings(UserID);
        string cacheKey = UserID + "UserSettings";
        if ((UserSettings != null) && (UserSettings.Rows.Count > 0))
        {
            HttpContext.Current.Session.Add(cacheKey, UserSettings.Rows[0][0].ToString());
            return false;
        }
        return true;
        //*********************************************************

        //UserSettings = DAL.GetUserSettings(UserID);
        //if ((UserSettings != null) && (UserSettings.Rows.Count > 0))
        //{
        //        HttpContext.Current.Session.Add("UserSettings", UserSettings.Rows[0][0].ToString());
        //        return false;
        //}
        //return true;
    }

    public static string GetUserSettings(string UserID)
    {
        DataAccess DAL = new DataAccess();
        //**********************************************
        string userSettings = String.Empty;
        string cacheKey = UserID + "UserSettings";
        if (HttpContext.Current.Session[cacheKey] != null)
        {
            userSettings = HttpContext.Current.Session[cacheKey].ToString();
        }
        else
        {
            DataTable dtUserSettings = DAL.GetUserSettings(UserID);
            if ((dtUserSettings != null) && (dtUserSettings.Rows.Count > 0))
            {
                userSettings = dtUserSettings.Rows[0][0].ToString();
                HttpContext.Current.Session.Add(cacheKey, userSettings);
            }
        }
        return userSettings;

        //**********************************************
        //string userSettings = String.Empty;
        //if (HttpContext.Current.Session["UserSettings"] != null)
        //{
        //    userSettings = HttpContext.Current.Session["UserSettings"].ToString();
        //}
        //else
        //{
        //    DataTable dtUserSettings = DAL.GetUserSettings(UserID);
        //    if ((dtUserSettings != null) && (dtUserSettings.Rows.Count > 0))
        //    {
        //        userSettings = dtUserSettings.Rows[0][0].ToString();
        //        HttpContext.Current.Session.Add("UserSettings", userSettings);
        //    }
        //}
        //return userSettings;
    }

    public static DataTable GetAllPatientModifiers()
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetAllPatientModifiers();
    }

    public static DataTable GetInsuranceInformation(long patientKey)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetInsuranceInformation(patientKey);
    }

    public static DataTable GetIdForLastPatientAccessed(string UserId)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetIdForLastPatientAccessed(UserId);
    }

    public static DataTable GetNRDList(long patientKey)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetNRDList(patientKey);
    }

    public static DataTable GetAMDForPatient(long patientKey)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetAMDForPatient(patientKey);
    }

    public static Color GetRowColorBasedOnDate(string SuppliedDate)
    {
        Color rowColor;

        ////Get the Date from the supplied DateString in MM-DD-YYYY format
        //DateTime maxOfCodeDate = GetDateFromParts(SuppliedDate);
        ////Now Get the Time Difference
        //TimeSpan ts = DateTime.Now.Date.Subtract(maxOfCodeDate.Date);
        //int distance = ts.Days;

        ////Set the BackGround Color based on Age of the record
        //if (distance < 365)
        //{
        //    rowColor = System.Drawing.ColorTranslator.FromHtml("#ff8282");
        //}
        //else if ((distance > 365) && (distance <= 730))
        //{
        //    rowColor = System.Drawing.ColorTranslator.FromHtml("#ffff99");
        //}
        //else if ((distance > 730) && (distance <= 1460))
        //{
        //    rowColor = System.Drawing.ColorTranslator.FromHtml("#82ff82");
        //}
        //else
        //{
        //    rowColor = System.Drawing.ColorTranslator.FromHtml("#8cdaff");
        //}


        //Get the Date from the supplied DateString in MM-DD-YYYY format
        DateTime maxOfCodeDate = GetDateFromParts(SuppliedDate);
        //Now Get the Time Difference
        TimeSpan ts = DateTime.Now.Date.Subtract(maxOfCodeDate.Date);
        int distance = ts.Days;

        rowColor = Color.Fuchsia; //default row color

        string userId = HttpContext.Current.User.Identity.Name.Trim().ToUpper();
        DataSet dsLegend = PatientManager.GetLegendByUserID(userId);
        if (dsLegend != null && dsLegend.Tables[0].Rows.Count > 0)
        {
            DateTime firstDate = Convert.ToDateTime(dsLegend.Tables[0].Rows[0]["FIRSTDATE"].ToString());
            TimeSpan dayDiff = DateTime.Now.Subtract(firstDate);

            int dayRange1 = dayDiff.Days;
            int dayRange2 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange2"].ToString());
            int dayRange3 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange3"].ToString());

            //int dayRange1 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange1"].ToString());
            //int dayRange2 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange2"].ToString());
            //int dayRange3 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange3"].ToString());

            if (distance <= dayRange1)
            {
                rowColor = System.Drawing.ColorTranslator.FromHtml("#ff8282");
            }
            else if ((distance > dayRange1) && (distance <= (dayRange1 + dayRange2)))
            {
                rowColor = System.Drawing.ColorTranslator.FromHtml("#F4A460");
            }
            else if ((distance > (dayRange1 + dayRange2)) && (distance <= (dayRange1 + dayRange2 + dayRange3)))
            {
                rowColor = System.Drawing.ColorTranslator.FromHtml("#82ff82");
            }
            else
            {
                rowColor = System.Drawing.ColorTranslator.FromHtml("#8cdaff");
            }
        }
        else
        {
            /*
                Date: 20 May 2010 By: Animesh
                Scenario: When Trial user login and if his/her legend is not set
                          then set 'Dr. Wayne Wells' ledgend as a default ledgend
            */
            if (userId == "TRIALUSER1" || userId == "TRIALUSER2" || userId == "TRIALUSER3")
            {
                string _defaultLedgendUser = "wayne";
                dsLegend = PatientManager.GetLegendByUserID(_defaultLedgendUser);

                if (dsLegend != null && dsLegend.Tables[0].Rows.Count > 0)
                {
                    DateTime firstDate = Convert.ToDateTime(dsLegend.Tables[0].Rows[0]["FIRSTDATE"].ToString());
                    TimeSpan dayDiff = DateTime.Now.Subtract(firstDate);

                    int dayRange1 = dayDiff.Days;
                    int dayRange2 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange2"].ToString());
                    int dayRange3 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange3"].ToString());

                    //int dayRange1 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange1"].ToString());
                    //int dayRange2 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange2"].ToString());
                    //int dayRange3 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange3"].ToString());

                    if (distance <= dayRange1)
                    {
                        rowColor = System.Drawing.ColorTranslator.FromHtml("#ff8282");
                    }
                    else if ((distance > dayRange1) && (distance <= (dayRange1 + dayRange2)))
                    {
                        rowColor = System.Drawing.ColorTranslator.FromHtml("#F4A460");
                    }
                    else if ((distance > (dayRange1 + dayRange2)) && (distance <= (dayRange1 + dayRange2 + dayRange3)))
                    {
                        rowColor = System.Drawing.ColorTranslator.FromHtml("#82ff82");
                    }
                    else
                    {
                        rowColor = System.Drawing.ColorTranslator.FromHtml("#8cdaff");
                    }
                }
            }
        }

        return rowColor;
    }

    public static DateTime GetMaxOfCodeDate(long patientKey, char Identifier)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetMaxOfCodeDate(patientKey, Identifier);
    }

    public static DateTime GetDateFromParts(string SuppliedDate)
    {
        string[] DateArray = SuppliedDate.Split(new char[] { '-' }); // MM-DD-YYYY format being supplied
        int Year = System.Convert.ToInt32(DateArray[2]);
        int Month = System.Convert.ToInt32(DateArray[0]);
        int Day = System.Convert.ToInt32(DateArray[1]);

        //Now Get the Time Difference
        DateTime returnDate = new DateTime(Year, Month, Day);
        return returnDate;
    }

    #region Configuration Methods

    #region Old Stuff...
    //public static List<Provider> GetAllProviders()
    //{
    //    return DAL.GetAllProviders();
    //}

    //public static DataTable GetDisciplines()
    //{
    //    return DAL.GetDisciplines();
    //}

    //public static void InsertProvider(string ProviderID, string LastName, string FirstName, string Phone, string Fax, 
    //                                    string CellPhone, string Pager, string InstitutionID, string InstitutionName, 
    //                                    double DisciplineCode, string Discipline, string WebSite, string Email )
    //{
    //    DAL.InsertProvider(ProviderID, LastName, FirstName, Phone, Fax, CellPhone, Pager, InstitutionID, InstitutionName, DisciplineCode, Discipline, WebSite, Email);
    //}
    #endregion

    public static object GetAllUsers()
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetAllUsers();
    }

    public static int InsertUser(Hashtable newValues, out string ErrorMessage)
    {
        DataAccess DAL = new DataAccess();
        return DAL.InsertUser(newValues, out ErrorMessage);
    }

    public static int UpdateUser(Hashtable newValues, string userID, out string ErrorMessage)
    {
        DataAccess DAL = new DataAccess();
        return DAL.UpdateUser(newValues, userID, out ErrorMessage);
    }

    public static int DeleteUser(string userID, out string ErrorMessage)
    {
        DataAccess DAL = new DataAccess();
        return DAL.DeleteUser(userID, out ErrorMessage);
    }

    public static DataTable GetAllProviders()
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetAllProviders();
    }

    public static DataTable GetDisciplines()
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetDisciplines();
    }

    public static int InsertProvider(Hashtable Values)
    {
        DataAccess DAL = new DataAccess();
        return DAL.InsertProvider(Values);
    }

    public static int UpdateProvider(Hashtable Values, string ProviderID)
    {
        DataAccess DAL = new DataAccess();
        return DAL.UpdateProvider(Values, ProviderID);
    }

    public static int DeleteProvider(string ProviderID)
    {
        DataAccess DAL = new DataAccess();
        return DAL.DeleteProvider(ProviderID);
    }
    #endregion

    public static DataTable GetPatientDemographicGeneralInfo(long patientKey)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientDemographicGeneralInfo(patientKey);
    }

    public static DataTable GetUserInfo(string UserName)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetUserInfo(UserName.ToLower());
    }
    public static DataSet GetRemarkableDisciplineDetail(string disCode)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetRemarkableDisciplineDetail(disCode);
    }
    public static DataTable GetProviderCodeDateHistory(string doctorId, long patientKey)
    {
        DataAccess DAL = new DataAccess();
        DataTable dt = DAL.GetProviderCodeDateHistory(doctorId, patientKey);
        if (DateRangeApplied)
        {
            DataTable tmpTable = dt.Copy();
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(tmpTable.Rows[i]["CODEDATE"].ToString().Trim());
                if (date > dateRangeStart || date < dateRangeEnd)
                {
                    dt.Rows[i].Delete();
                }
            }
            dt.AcceptChanges();
        }
        return dt;
    }
    public static DataTable GetInstitutionCodeDateHistory(string institutionID, long patientKey)
    {
        DataAccess DAL = new DataAccess();
        DataTable dt = DAL.GetInstitutionCodeDateHistory(institutionID, patientKey);
        if (DateRangeApplied)
        {
            DataTable tmpTable = dt.Copy();
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(tmpTable.Rows[i]["CODEDATE"].ToString().Trim());
                if (date > dateRangeStart || date < dateRangeEnd)
                {
                    dt.Rows[i].Delete();
                }
            }
            dt.AcceptChanges();
        }
        return dt;
    }
    public static string GetMinDate(int discode, long patientKey)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetMinDate(discode, patientKey);
    }
    public static string GetMaxDate(int discode, long patientKey)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetMaxDate(discode, patientKey);
    }
    public static DataSet GetLegendByUserID(string UserId)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetLegendByUserID(UserId);
    }
    public static void UpdateLegend(string userID, DateTime dayRange1, int dayRange2, int dayRange3, int dayRange4)
    {
        DataAccess DAL = new DataAccess();
        DAL.UpdateLegend(userID, dayRange1, dayRange2, dayRange3, dayRange4);
    }
    //public static void UpdateLegend(string userID, int dayRange1, int dayRange2, int dayRange3, int dayRange4)
    //{
    //    DataAccess DAL = new DataAccess();
    //    DAL.UpdateLegend(userID, dayRange1, dayRange2, dayRange3, dayRange4);
    //}

    public static void LegendAddNewDefault(string userId)
    {
        DateTime today = DateTime.Now;
        today = today.AddDays(-365);

        DataAccess DAL = new DataAccess();
        DAL.LegendAdNew(userId, today.Date, 365, 730, 0);
    }

    public static void LegendAdNew(string userID, DateTime dayRange1, int dayRange2, int dayRange3, int dayRange4)
    {
        DataAccess DAL = new DataAccess();
        DAL.LegendAdNew(userID, dayRange1, dayRange2, dayRange3, dayRange4);
    }
    //public static void LegendAdNew2(string userID, int dayRange1, int dayRange2, int dayRange3, int dayRange4)
    //{
    //    DataAccess DAL = new DataAccess();
    //    DAL.LegendAdNew(userID, dayRange1, dayRange2, dayRange3, dayRange4);
    //}
    public static DataSet GetInstitutionInfo(string institutionID)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetInstitutionInfo(institutionID);
    }
    public static DataSet GetInstitutionsForPatients(long patientKey)
    {
        DataAccess DAL = new DataAccess();
        if (DateRangeApplied)
        {
            return DAL.GetInstitutionsForPatients(patientKey, dateRangeStart, dateRangeEnd);
        }
        else
        {
            return DAL.GetInstitutionsForPatients(patientKey);
        }
    }

    public static DataTable GetLastDateAndFirstDate(long patientKey)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetLastDateAndFirstDate(patientKey);
    }

    public static void UpdatePatientAccessRecordUpdateTime(long patientKey, string userid, DateTime lastAccessTime)
    {
        DataAccess DAL = new DataAccess();
        DAL.UpdatePatientAccessRecordUpdateTime(patientKey, userid, lastAccessTime);
    }
    public static DataSet GetCodeModifiersByMedcode(string codeType, string medcode, int codeVersion)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetCodeModifiersByMedcode(codeType, medcode, codeVersion);
    }
    public static DataSet GetUsagesLogins()
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetUsagesLogins();
    }
    public static DataSet GetUsagesLoginsByUserID(string userId)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetUsagesLoginsByUserID(userId);
    }
    public static DataSet GetUsagesPatientAccess(string userId, DateTime min, DateTime max)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetUsagesPatientAccess(userId, min, max);
    }
    public static void UsagesLoginAdd(string userId)
    {
        DataAccess DAL = new DataAccess();
        DAL.UsagesLoginAdd(userId);
    }

    public static DataTable GetPatientSearchPageData(string query, int currentPage, int pageSize, int option)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientSearchPageData(query, currentPage, pageSize, option);
    }
    public static int GetPatientSearchPageCount(string query, int pageSize, int option)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientSearchPageCount(query, pageSize, option);
    }
    public static int GetPatientSearchRecordCount(string query, int option)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientSearchRecordCount(query, option);
    }
    public static DataTable GetPatientEducation(long patientKey)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientEducation(patientKey);
    }
    public static DataTable GetPatientEmployment(long patientKey)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientEmployment(patientKey);
    }
    public static int GetAnalyzerCommonDiagnosesCount(char sex, int ageFrom, int ageTo)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetAnalyzerCommonDiagnosesCount(sex, ageFrom, ageTo);
    }
    public static DataSet GetAnalyzerCommonDiagnoses(char sex, int ageFrom, int ageTo, int pageSize, int currentPage)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetAnalyzerCommonDiagnoses(sex, ageFrom, ageTo, pageSize, currentPage);
    }
    public static int GetAnalyzerPatientByMedcodeAgeSexCount(char sex, int ageFrom, int ageTo, string code_type, string medcode, int codeversion)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetAnalyzerPatientByMedcodeAgeSexCount(sex, ageFrom, ageTo, code_type, medcode, codeversion);
    }
    public static DataTable GetAnalyzerPatientByMedcodeAgeSex(char sex, int ageFrom, int ageTo, string code_type, string medcode, int codeversion, int pageSize, int currentPage)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetAnalyzerPatientByMedcodeAgeSex(sex, ageFrom, ageTo, code_type, medcode, codeversion, pageSize, currentPage);
    }
    public static DataTable GetCodeDescriptinChangeHistory(string codeType, string medcode)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetCOdeDescriptinChangeHistory(codeType, medcode);
    }
    public static DataTable GetCodeHistory(string codeType, string medcode)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetCodeHistory(codeType, medcode);
    }


    #region Alert Related
    public static int AddVitalSigns(long patientKey, DateTime date, string temperature, char tempUnit, string bloodPressureSys, string bloodPressureDia, string pulse, string pulseType, string respiratoryRate, string chiefComplain, string BMIWeight, string BMIHeight, string painScale, string painLocation, string pupilSizeRight, string pupilSizeLeft, string bloodGlucoseLevel, out string ErrorMessage)
    {
        DataAccess DAL = new DataAccess();
        return DAL.AddVitalSigns(patientKey, date, temperature, tempUnit, bloodPressureSys, bloodPressureDia, pulse, pulseType, respiratoryRate, chiefComplain, BMIWeight, BMIHeight, painScale, painLocation, pupilSizeRight, pupilSizeLeft, bloodGlucoseLevel, out  ErrorMessage);
    }

    public static DataSet GetPatientVitalSigns(long patientKey)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientVitalSigns(patientKey);
    }

    #region AddVitalSigns
    public static int AddVitalSigns(long patientKey,
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
        DataAccess DAL = new DataAccess();
        return DAL.AddVitalSigns(patientKey,
                                record_date,
                                temperature,
                                tempUnit,
                                bloodPressureSys,
                                bloodPressureDia,
                                pulse,
                                pulseType,
                                respiratoryRate,
                                chiefComplain,
                                BMIWeight,
                                BMIHeight,
                                painScale,
                                painLocation,
                                pupilSizeRight,
                                pupilSizeLeft,
                                bloodGlucoseLevel,
                                patient_image,
                                out identiy,
                                out ErrorMessage);
    } 
    #endregion

    #region AddAlerts
    public static int AddAlerts(DateTime nurse_soap_time_stamp,
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
        DataAccess DAL = new DataAccess();
        return DAL.AddAlerts(nurse_soap_time_stamp,
                                has_nurse_soap,
                                nurse_soap_escalation_count,
                                patientKey,
                                doctor_soap_time_stamp,
                                has_doctor_soap,
                                is_dropped,
                                alert_status,
                                is_active,
                                out identiy,
                                out ErrorMessage);
    }
    #endregion

    #region AddSoaps
    public static int AddSoaps(long? owner_id,
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
        string doc_radiology,
        string doc_performance_measurement,
        string doc_emerging_tech_sv,
        string doc_other_text,
        string doc_followup,
        string doc_other,
        out long identiy,
        out string ErrorMessage)
    {
        DataAccess DAL = new DataAccess();
        return DAL.AddSoaps(owner_id,
                                patient_key,
                                date_added,
                                code_type,
                                doctor_id,
                                nurse_id,
                                recorded_vitalsigns_id,
                                alert_id,
                                subjective,
                                objective,
                                assessment,
                                notes,
                                umr_plan_id,
                                doc_problems,
                                doc_prescription,
                                doc_diagnostictest,
                                doc_lab,
                                doc_procedures,
                                doc_immunization,
                                doc_pat_educations,
                                doc_respond,
                                doc_refer,
                                 examined_by_provider,
                                  doc_radiology,
                                 doc_performance_measurement,
                                 doc_emerging_tech_sv,
                                 doc_other_text,
                                 doc_followup,
                                 doc_other,
                                out identiy,
                                out ErrorMessage);
    }
    #endregion

    #region AddSoapsAddress
    public static int AddSoapsAddress(long soaps_id,
        string emailaddress,
        string smsnumber,
        string doctor_id,
        string nurse_id,
        string institution_id,
        out long identiy,
        out string ErrorMessage)
    {
        DataAccess DAL = new DataAccess();
        return DAL.AddSoapsAddress(soaps_id,
                                emailaddress,
                                smsnumber,
                                doctor_id,
                                nurse_id,
                                institution_id,
                                out identiy,
                                out ErrorMessage);
    }
    #endregion


    #region AddPatientDiagonses
    public static int AddPatientDiagonses(long patient_key,
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
        DataAccess DAL = new DataAccess();
        return DAL.AddPatientDiagonses(patient_key,
        code_type,
        code_version,
        medcode,
        codedate,
         hospitalization,
        institution_code,
        doctor_id,
        visibility,
        out ErrorMessage);
    }
    #endregion

    #region AddAlertEmail
    public static int AddAlertEmail(long ALERT_ID,
        string ALERT_EMAIL_BODY,
        string EMAIL_RECIEPENTS,
        out string ErrorMessage)
    {
        DataAccess DAL = new DataAccess();
        return DAL.AddAlertEmail(ALERT_ID,
        ALERT_EMAIL_BODY,
        EMAIL_RECIEPENTS,
        out ErrorMessage);
    }
    #endregion

    public static DataSet GetPatientVitalSignsByID(int id)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientVitalSignsByID(id);
    }

    public static DataSet GetPatientSoapsByAlertAndNurseID(long alert_id, string nurse_id)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientSoapsByAlertAndNurseID(alert_id, nurse_id);
    }


    public static DataSet GetPatientPatientSoapsAddressBySoapID(long soaps_id)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientPatientSoapsAddressBySoapID(soaps_id);
    }


    public static DataSet GetPlans()
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPlans();
    }

    public static DataSet GetPatientNurseSoapsByAlertID(long alert_id)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientNurseSoapsByAlertID(alert_id);
    }
    public static DataSet GetPatientSoapsBySOAPID(long soaps_id)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientSoapsBySOAPID(soaps_id);
    }
    public static DataSet GetPatientGetActiveAlert(long PATIENT_KEY, byte ALERT_STATUS)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientGetActiveAlert(PATIENT_KEY,
                                ALERT_STATUS);
    }
    public static int GetPatientGetActiveAlertCount(long PATIENT_KEY, byte ALERT_STATUS)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientGetActiveAlertCount(PATIENT_KEY,
                                ALERT_STATUS);
    }



    public static DataSet GetPatientDOCSoapsByAlertID(long alert_id)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientDOCSoapsByAlertID(alert_id);
    }
    public static int GetPatientGetActiveDOCAlertCount(long PATIENT_KEY, byte ALERT_STATUS)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientGetActiveDOCAlertCount(PATIENT_KEY,
                                ALERT_STATUS);
    }
    public static DataSet GetPatientGetActiveDOCAlert(long PATIENT_KEY, byte ALERT_STATUS)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetPatientGetActiveDOCAlert(PATIENT_KEY,
                                ALERT_STATUS);
    }


    #region UpdateAlerts
    public static int UpdateAlertWithDoctorResponse(long alert_id,
        DateTime? doctor_soap_time_stamp,
        bool? has_doctor_soap,
        byte alert_status,
        bool is_active,
        out string ErrorMessage)
    {
        DataAccess DAL = new DataAccess();
        return DAL.UpdateAlertWithDoctorResponse(alert_id,
                                doctor_soap_time_stamp,
                                has_doctor_soap,
                                alert_status,
                                is_active,
                                out ErrorMessage);
    }
    #endregion

    public static void SendReportTo(long patientKey, string email)
    {
        StringBuilder sb = new StringBuilder();

        DataSet dsPatDemo = PatientManager.GetPatientDemographics(patientKey, HttpContext.Current.User.Identity.Name);

        if (dsPatDemo != null && dsPatDemo.Tables.Count > 0)
        {
            DataRow row = dsPatDemo.Tables[0].Rows[0];

            sb.Append("<h3>Patient Demographic Information</h3>");

            sb.Append((String.IsNullOrEmpty(row["PatientID"].ToString()) ? "" : "Patient Id:" + row["PatientID"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Prefix"].ToString()) ? "" : "Prefix: " + row["Prefix"].ToString() + "<br>"));

            sb.Append(String.IsNullOrEmpty(row["FirstName"].ToString()) ? "" : "First Name:" + row["FirstName"].ToString() + "<br>");
            sb.Append((String.IsNullOrEmpty(row["MiddleName"].ToString()) ? "" : "Middle Name:" + row["MiddleName"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["LastName"].ToString()) ? "" : "Last Name:" + row["LastName"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Suffix"].ToString()) ? "" : "Suffix:" + row["Suffix"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["StreetAddress"].ToString()) ? "" : "Street Address:" + row["StreetAddress"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["CityTown"].ToString()) ? "" : "City/Town:" + row["CityTown"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Country"].ToString()) ? "" : "County: " + row["Country"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["State"].ToString()) ? "" : "State: " + row["State"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Zip"].ToString()) ? "" : "Zip Code:" + row["Zip"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["County"].ToString()) ? "" : "Country:" + row["County"].ToString() + "<br>"));


            sb.Append((row["HomePhone"] == DBNull.Value) ? "Home Phone:" + "N/A<br>" : "Home Phone:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["HomePhone"])) + "<br>");
            sb.Append((row["BusinessPhone"] == DBNull.Value) ? "Business Phone:" + "N/A<br>" : "Business Phone:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["BusinessPhone"])) + "<br>");
            sb.Append((row["CellPhone"] == DBNull.Value) ? "Mobile Phone:" + "N/A<br>" : "Mobile Phone:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["CellPhone"])) + "<br>");
            sb.Append((row["Pager"] == DBNull.Value) ? "Pager Number:" + "N/A<br>" : "Pager Number:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["Pager"])) + "<br>");


            sb.Append((String.IsNullOrEmpty(row["PhysicalMarks"].ToString()) ? "" : "Physical Marks:" + row["PhysicalMarks"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["DateOfBirth"].ToString()) ? "" : "Date of Birth:" + row["DateOfBirth"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["BirthPlace"].ToString()) ? "" : "Birth Place:" + row["BirthPlace"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Sex"].ToString()) ? "" : "Sex:" + row["Sex"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["MaritalStatus"].ToString()) ? "" : "Marital Status:" + row["MaritalStatus"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["LanguagesSpoken"].ToString()) ? "" : "Languages Spoken:" + row["LanguagesSpoken"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Religion"].ToString()) ? "" : "Religion:" + row["Religion"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["BloodType"].ToString()) ? "" : "Blood Type:" + row["BloodType"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Occupation"].ToString()) ? "" : "Occupation:" + row["Occupation"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["HairColor"].ToString()) ? "" : "Hair Color:" + row["HairColor"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["EyeColor"].ToString()) ? "" : "Eye Color:" + row["EyeColor"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Race"].ToString()) ? "" : "Race:" + row["Race"].ToString() + "<br>"));

        }

        DataSet dsPatVital = PatientManager.GetPatientVitalSigns(patientKey);

        if (dsPatVital != null && dsPatVital.Tables.Count > 0)
        {
            DataRow row = dsPatVital.Tables[0].Rows[0];


            sb.Append("<h3>Patient Vital Signs</h3>");
            sb.Append("Chief Complain:" + row["CHIEF_COMPLAINT"].ToString() + "<br>");
            sb.Append("Temperature:" + row["TEMPERATURE"].ToString() + "°" + row["TEMPERATURE_UNIT"].ToString() + "<br>");
            sb.Append("Blood Pressure:" + row["BLOOD_PRESSURE_SYSTOLIC"].ToString() + "(systolic) " + row["BLOOD_PRESSURE_DIASTOLIC"].ToString() + "(diastolic)" + "<br>");
            sb.Append("Pulse:" + row["PULSE"].ToString() + " beats per minute " + row["PULSE_TYPE"].ToString() + "<br>");
            sb.Append("Respiratory Rate:" + row["RESPIRATORY_RATE"].ToString() + " beats per minute " + "<br>");
            sb.Append("BMI:" + "Weight:" + row["BMI_WEIGHT"].ToString() + " , Height:" + row["BMI_HEIGHT"].ToString() + "<br>");
        }

        DataSet dsMPIInfo = PatientManager.GetMPIInfo(patientKey);
        if (dsMPIInfo != null && dsMPIInfo.Tables.Count > 0)
        {
            sb.Append("<h3>Master Patient Code Index</h3>");
            sb.Append("<table>");
            sb.Append("<thead>");
            sb.Append("<tr><th>Code</th><th>Type</th><th>Version</th><th>Medical Content Index</th><th>Service Date</th><th>Time(EST)</th><th>Provider ID</th><th>Healthcare Facility ID</th></tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            for (int i = 0; i < dsMPIInfo.Tables[0].Rows.Count; i++)
            {
                DataRow row = dsMPIInfo.Tables[0].Rows[i];
                sb.Append("<tr>");
                sb.Append("<td>" + row["Code"] + "</td>");
                //sb.Append("<td>" + row["Modifier"] + "</td>");
                sb.Append("<td>" + row["Type"] + "</td>");
                sb.Append("<td>" + row["Version"] + "</td>");
                sb.Append("<td>" + row["MedicalContentIndex"] + "</td>");
                sb.Append("<td>" + row["DateOfService"] + "</td>");
                sb.Append("<td>" + row["ServiceTime"] + "</td>");
                sb.Append("<td>" + row["ProviderID"] + "</td>");
                sb.Append("<td>" + row["InstituteCode"] + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
        }


        sb.Append("<a href=\"#\" target=\"_blank\">Acknowledge Receipt</a>");
        string mailBody = sb.ToString();
        EmailManager client = new EmailManager(email, "Patient Report", true, mailBody);
        client.Send();
    }
    public static void SendSMSTo(long patientKey, List<string> phones)
    {
        StringBuilder sb = new StringBuilder();

        DataSet dsPatDemo = PatientManager.GetPatientDemographics(patientKey, HttpContext.Current.User.Identity.Name);

        if (dsPatDemo != null && dsPatDemo.Tables.Count > 0)
        {
            DataRow row = dsPatDemo.Tables[0].Rows[0];

            sb.Append("<h3>Patient Demographic Information</h3>");

            sb.Append((String.IsNullOrEmpty(row["PatientID"].ToString()) ? "" : "Patient Id:" + row["PatientID"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Prefix"].ToString()) ? "" : "Prefix: " + row["Prefix"].ToString() + "<br>"));

            sb.Append(String.IsNullOrEmpty(row["FirstName"].ToString()) ? "" : "First Name:" + row["FirstName"].ToString() + "<br>");
            sb.Append((String.IsNullOrEmpty(row["MiddleName"].ToString()) ? "" : "Middle Name:" + row["MiddleName"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["LastName"].ToString()) ? "" : "Last Name:" + row["LastName"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Suffix"].ToString()) ? "" : "Suffix:" + row["Suffix"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["StreetAddress"].ToString()) ? "" : "Street Address:" + row["StreetAddress"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["CityTown"].ToString()) ? "" : "City/Town:" + row["CityTown"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Country"].ToString()) ? "" : "County: " + row["Country"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["State"].ToString()) ? "" : "State: " + row["State"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Zip"].ToString()) ? "" : "Zip Code:" + row["Zip"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["County"].ToString()) ? "" : "Country:" + row["County"].ToString() + "<br>"));


            sb.Append((row["HomePhone"] == DBNull.Value) ? "Home Phone:" + "N/A<br>" : "Home Phone:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["HomePhone"])) + "<br>");
            sb.Append((row["BusinessPhone"] == DBNull.Value) ? "Business Phone:" + "N/A<br>" : "Business Phone:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["BusinessPhone"])) + "<br>");
            sb.Append((row["CellPhone"] == DBNull.Value) ? "Mobile Phone:" + "N/A<br>" : "Mobile Phone:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["CellPhone"])) + "<br>");
            sb.Append((row["Pager"] == DBNull.Value) ? "Pager Number:" + "N/A<br>" : "Pager Number:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["Pager"])) + "<br>");


            sb.Append((String.IsNullOrEmpty(row["PhysicalMarks"].ToString()) ? "" : "Physical Marks:" + row["PhysicalMarks"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["DateOfBirth"].ToString()) ? "" : "Date of Birth:" + row["DateOfBirth"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["BirthPlace"].ToString()) ? "" : "Birth Place:" + row["BirthPlace"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Sex"].ToString()) ? "" : "Sex:" + row["Sex"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["MaritalStatus"].ToString()) ? "" : "Marital Status:" + row["MaritalStatus"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["LanguagesSpoken"].ToString()) ? "" : "Languages Spoken:" + row["LanguagesSpoken"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Religion"].ToString()) ? "" : "Religion:" + row["Religion"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["BloodType"].ToString()) ? "" : "Blood Type:" + row["BloodType"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Occupation"].ToString()) ? "" : "Occupation:" + row["Occupation"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["HairColor"].ToString()) ? "" : "Hair Color:" + row["HairColor"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["EyeColor"].ToString()) ? "" : "Eye Color:" + row["EyeColor"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Race"].ToString()) ? "" : "Race:" + row["Race"].ToString() + "<br>"));

        }

        DataSet dsPatVital = PatientManager.GetPatientVitalSigns(patientKey);

        if (dsPatVital != null && dsPatVital.Tables.Count > 0)
        {
            DataRow row = dsPatVital.Tables[0].Rows[0];


            sb.Append("<h3>Patient Vital Signs</h3>");
            sb.Append("Chief Complain:" + row["CHIEF_COMPLAINT"].ToString() + "<br>");
            sb.Append("Temperature:" + row["TEMPERATURE"].ToString() + "°" + row["TEMPERATURE_UNIT"].ToString() + "<br>");
            sb.Append("Blood Pressure:" + row["BLOOD_PRESSURE_SYSTOLIC"].ToString() + "(systolic) " + row["BLOOD_PRESSURE_DIASTOLIC"].ToString() + "(diastolic)" + "<br>");
            sb.Append("Pulse:" + row["PULSE"].ToString() + " beats per minute " + row["PULSE_TYPE"].ToString() + "<br>");
            sb.Append("Respiratory Rate:" + row["RESPIRATORY_RATE"].ToString() + " beats per minute " + "<br>");
            sb.Append("BMI:" + "Weight:" + row["BMI_WEIGHT"].ToString() + " , Height:" + row["BMI_HEIGHT"].ToString() + "<br>");
        }

        DataSet dsMPIInfo = PatientManager.GetMPIInfo(patientKey);
        if (dsMPIInfo != null && dsMPIInfo.Tables.Count > 0)
        {
            sb.Append("<h3>Master Patient Code Index</h3>");
            sb.Append("<table>");
            sb.Append("<thead>");
            sb.Append("<tr><th>Code</th><th>Type</th><th>Version</th><th>Medical Content Index</th><th>Service Date</th><th>Time(EST)</th><th>Provider ID</th><th>Healthcare Facility ID</th></tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            for (int i = 0; i < dsMPIInfo.Tables[0].Rows.Count; i++)
            {
                DataRow row = dsMPIInfo.Tables[0].Rows[i];
                sb.Append("<tr>");
                sb.Append("<td>" + row["Code"] + "</td>");
                //sb.Append("<td>" + row["Modifier"] + "</td>");
                sb.Append("<td>" + row["Type"] + "</td>");
                sb.Append("<td>" + row["Version"] + "</td>");
                sb.Append("<td>" + row["MedicalContentIndex"] + "</td>");
                sb.Append("<td>" + row["DateOfService"] + "</td>");
                sb.Append("<td>" + row["ServiceTime"] + "</td>");
                sb.Append("<td>" + row["ProviderID"] + "</td>");
                sb.Append("<td>" + row["InstituteCode"] + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
        }


        string smsBody = sb.ToString();
        DataAccess DAL = new DataAccess();
        int reportId = DAL.AddReportSMS(smsBody);

        string smsText = "Patient Report. http://www.universalmedicalrecord.com/UMR.WebClient/ShowReport.aspx?id=" + reportId;
            //"Patient Report. http://www.universalmedicalrecord.com/UMR.WEB/ShowReport.aspx?id=" + reportId;
        //"Patient Report. http://www.umrtest.com/UMR.WebClient/ShowReport.aspx?id=" + reportId;
        foreach (string phone in phones)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            byte[] data = client.DownloadData("http://127.0.0.1:8080/googlevoice/index.php?phone=" + phone + "&sms=" + smsText + "");
        }

    }
    public static void SendSMSTo(List<string> phones, string smsText )
    {
        foreach (string phone in phones)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            //byte[] data = client.DownloadData("http://127.0.0.1:8080/googlevoice/index.php?phone=" + phone + "&sms=" + smsText + "");

            byte[] data = client.DownloadData("http://localhost/googlevoice/index.php?phone=" + phone + "&sms=" + smsText + "");
        }

    }

    public static void SendEmailTo(long patientKey, string email)
    {
        StringBuilder sb = new StringBuilder();

        DataSet dsPatDemo = PatientManager.GetPatientDemographics(patientKey, HttpContext.Current.User.Identity.Name);

        if (dsPatDemo != null && dsPatDemo.Tables.Count > 0)
        {
            DataRow row = dsPatDemo.Tables[0].Rows[0];

            sb.Append("<h3>Patient Demographic Information</h3>");

            sb.Append((String.IsNullOrEmpty(row["PatientID"].ToString()) ? "" : "Patient Id:" + row["PatientID"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Prefix"].ToString()) ? "" : "Prefix: " + row["Prefix"].ToString() + "<br>"));

            sb.Append(String.IsNullOrEmpty(row["FirstName"].ToString()) ? "" : "First Name:" + row["FirstName"].ToString() + "<br>");
            sb.Append((String.IsNullOrEmpty(row["MiddleName"].ToString()) ? "" : "Middle Name:" + row["MiddleName"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["LastName"].ToString()) ? "" : "Last Name:" + row["LastName"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Suffix"].ToString()) ? "" : "Suffix:" + row["Suffix"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["StreetAddress"].ToString()) ? "" : "Street Address:" + row["StreetAddress"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["CityTown"].ToString()) ? "" : "City/Town:" + row["CityTown"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Country"].ToString()) ? "" : "County: " + row["Country"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["State"].ToString()) ? "" : "State: " + row["State"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Zip"].ToString()) ? "" : "Zip Code:" + row["Zip"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["County"].ToString()) ? "" : "Country:" + row["County"].ToString() + "<br>"));


            sb.Append((row["HomePhone"] == DBNull.Value) ? "Home Phone:" + "N/A<br>" : "Home Phone:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["HomePhone"])) + "<br>");
            sb.Append((row["BusinessPhone"] == DBNull.Value) ? "Business Phone:" + "N/A<br>" : "Business Phone:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["BusinessPhone"])) + "<br>");
            sb.Append((row["CellPhone"] == DBNull.Value) ? "Mobile Phone:" + "N/A<br>" : "Mobile Phone:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["CellPhone"])) + "<br>");
            sb.Append((row["Pager"] == DBNull.Value) ? "Pager Number:" + "N/A<br>" : "Pager Number:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["Pager"])) + "<br>");


            sb.Append((String.IsNullOrEmpty(row["PhysicalMarks"].ToString()) ? "" : "Physical Marks:" + row["PhysicalMarks"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["DateOfBirth"].ToString()) ? "" : "Date of Birth:" + row["DateOfBirth"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["BirthPlace"].ToString()) ? "" : "Birth Place:" + row["BirthPlace"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Sex"].ToString()) ? "" : "Sex:" + row["Sex"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["MaritalStatus"].ToString()) ? "" : "Marital Status:" + row["MaritalStatus"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["LanguagesSpoken"].ToString()) ? "" : "Languages Spoken:" + row["LanguagesSpoken"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Religion"].ToString()) ? "" : "Religion:" + row["Religion"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["BloodType"].ToString()) ? "" : "Blood Type:" + row["BloodType"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Occupation"].ToString()) ? "" : "Occupation:" + row["Occupation"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["HairColor"].ToString()) ? "" : "Hair Color:" + row["HairColor"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["EyeColor"].ToString()) ? "" : "Eye Color:" + row["EyeColor"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Race"].ToString()) ? "" : "Race:" + row["Race"].ToString() + "<br>"));

        }

        DataSet dsPatVital = PatientManager.GetPatientVitalSigns(patientKey);

        if (dsPatVital != null && dsPatVital.Tables.Count > 0)
        {
            DataRow row = dsPatVital.Tables[0].Rows[0];


            sb.Append("<h3>Patient Vital Signs</h3>");
            sb.Append("Chief Complain:" + row["CHIEF_COMPLAINT"].ToString() + "<br>");
            sb.Append("Temperature:" + row["TEMPERATURE"].ToString() + "°" + row["TEMPERATURE_UNIT"].ToString() + "<br>");
            sb.Append("Blood Pressure:" + row["BLOOD_PRESSURE_SYSTOLIC"].ToString() + "(systolic) " + row["BLOOD_PRESSURE_DIASTOLIC"].ToString() + "(diastolic)" + "<br>");
            sb.Append("Pulse:" + row["PULSE"].ToString() + " beats per minute " + row["PULSE_TYPE"].ToString() + "<br>");
            sb.Append("Respiratory Rate:" + row["RESPIRATORY_RATE"].ToString() + " beats per minute " + "<br>");
            sb.Append("BMI:" + "Weight:" + row["BMI_WEIGHT"].ToString() + " , Height:" + row["BMI_HEIGHT"].ToString() + "<br>");
        }

        DataSet dsMPIInfo = PatientManager.GetMPIInfo(patientKey);
        if (dsMPIInfo != null && dsMPIInfo.Tables.Count > 0)
        {
            sb.Append("<h3>Master Patient Code Index</h3>");
            sb.Append("<table>");
            sb.Append("<thead>");
            sb.Append("<tr><th>Code</th><th>Type</th><th>Version</th><th>Medical Content Index</th><th>Service Date</th><th>Time(EST)</th><th>Provider ID</th><th>Healthcare Facility ID</th></tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            for (int i = 0; i < dsMPIInfo.Tables[0].Rows.Count; i++)
            {
                DataRow row = dsMPIInfo.Tables[0].Rows[i];
                sb.Append("<tr>");
                sb.Append("<td>" + row["Code"] + "</td>");
                //sb.Append("<td>" + row["Modifier"] + "</td>");
                sb.Append("<td>" + row["Type"] + "</td>");
                sb.Append("<td>" + row["Version"] + "</td>");
                sb.Append("<td>" + row["MedicalContentIndex"] + "</td>");
                sb.Append("<td>" + row["DateOfService"] + "</td>");
                sb.Append("<td>" + row["ServiceTime"] + "</td>");
                sb.Append("<td>" + row["ProviderID"] + "</td>");
                sb.Append("<td>" + row["InstituteCode"] + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
        }


        sb.Append("<a href=\"#\" target=\"_blank\">Acknowledge Receipt</a>");
        string mailBody = sb.ToString();
        EmailManager client = new EmailManager(email, "Patient Report", true, mailBody);
        client.Send();
    }

    public static void SendEmailTo(long patientKey, string email,int vitalID, long soapID, long? alertID)
    {
        StringBuilder sb = new StringBuilder();

        DataSet dsPatDemo = PatientManager.GetPatientDemographics(patientKey, HttpContext.Current.User.Identity.Name);

        DataSet dsPatVital = PatientManager.GetPatientVitalSignsByID(vitalID);

        if (dsPatDemo != null && dsPatDemo.Tables.Count > 0)
        {
            DataRow row = dsPatDemo.Tables[0].Rows[0];

            sb.Append("<h3>Patient Demographic Information</h3>");

            if (dsPatVital != null && dsPatVital.Tables.Count > 0)
            {
                if (dsPatVital.Tables[0].Rows[0]["PATIENT_IMAGE"] != null && dsPatVital.Tables[0].Rows[0]["PATIENT_IMAGE"] != DBNull.Value)
                {
                    sb.Append("<h3>Patient Image:</h3>");
                    sb.Append("<img src=\"" + "http://www.universalmedicalrecord.com/umr.webclient/Oracle/PatientImage.aspx?VitalSignID=" + vitalID.ToString() + "\"" + " alt=" + "\"No Image Has been uploaded\"" + "style=\"border-width:0px;\"" + ">" + "<br>");
                }
            }

            sb.Append((String.IsNullOrEmpty(row["PatientID"].ToString()) ? "" : "Patient Id:" + row["PatientID"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Prefix"].ToString()) ? "" : "Prefix: " + row["Prefix"].ToString() + "<br>"));

            sb.Append(String.IsNullOrEmpty(row["FirstName"].ToString()) ? "" : "First Name:" + row["FirstName"].ToString() + "<br>");
            sb.Append((String.IsNullOrEmpty(row["MiddleName"].ToString()) ? "" : "Middle Name:" + row["MiddleName"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["LastName"].ToString()) ? "" : "Last Name:" + row["LastName"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Suffix"].ToString()) ? "" : "Suffix:" + row["Suffix"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["StreetAddress"].ToString()) ? "" : "Street Address:" + row["StreetAddress"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["CityTown"].ToString()) ? "" : "City/Town:" + row["CityTown"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Country"].ToString()) ? "" : "County: " + row["Country"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["State"].ToString()) ? "" : "State: " + row["State"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Zip"].ToString()) ? "" : "Zip Code:" + row["Zip"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["County"].ToString()) ? "" : "Country:" + row["County"].ToString() + "<br>"));


            sb.Append((row["HomePhone"] == DBNull.Value) ? "Home Phone:" + "N/A<br>" : "Home Phone:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["HomePhone"])) + "<br>");
            sb.Append((row["BusinessPhone"] == DBNull.Value) ? "Business Phone:" + "N/A<br>" : "Business Phone:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["BusinessPhone"])) + "<br>");
            sb.Append((row["CellPhone"] == DBNull.Value) ? "Mobile Phone:" + "N/A<br>" : "Mobile Phone:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["CellPhone"])) + "<br>");
            sb.Append((row["Pager"] == DBNull.Value) ? "Pager Number:" + "N/A<br>" : "Pager Number:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["Pager"])) + "<br>");


            sb.Append((String.IsNullOrEmpty(row["PhysicalMarks"].ToString()) ? "" : "Physical Marks:" + row["PhysicalMarks"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["DateOfBirth"].ToString()) ? "" : "Date of Birth:" + row["DateOfBirth"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["BirthPlace"].ToString()) ? "" : "Birth Place:" + row["BirthPlace"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Sex"].ToString()) ? "" : "Sex:" + row["Sex"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["MaritalStatus"].ToString()) ? "" : "Marital Status:" + row["MaritalStatus"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["LanguagesSpoken"].ToString()) ? "" : "Languages Spoken:" + row["LanguagesSpoken"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Religion"].ToString()) ? "" : "Religion:" + row["Religion"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["BloodType"].ToString()) ? "" : "Blood Type:" + row["BloodType"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Occupation"].ToString()) ? "" : "Occupation:" + row["Occupation"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["HairColor"].ToString()) ? "" : "Hair Color:" + row["HairColor"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["EyeColor"].ToString()) ? "" : "Eye Color:" + row["EyeColor"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Race"].ToString()) ? "" : "Race:" + row["Race"].ToString() + "<br>"));

        }
        
            DataSet dsSoap = PatientManager.GetPatientSoapsBySOAPID(soapID);
            if (dsSoap != null && dsSoap.Tables.Count > 0)
            {
                DataRow row = dsSoap.Tables[0].Rows[0];

                if (row["NURSE_ID"] != null && row["NURSE_ID"] != DBNull.Value)
                {
                    sb.Append("<h3>Nurse's S.O.A.P</h3>");
                }
                else
                {
                    sb.Append("<h3>Doctor's S.O.A.P</h3>");
                }

                sb.Append("Subjective: " + row["SUBJECTIVE"].ToString() + "<br>");
                sb.Append("Objective: " + row["OBJECTIVE"].ToString() + "<br>");
                sb.Append("Assesment: " + row["ASSESSMENT"].ToString() + "<br>");
                if (row["NURSE_ID"] != null && row["NURSE_ID"] != DBNull.Value)
                {
                    if (row["NOTES"] != null)
                    {
                        sb.Append("Notes: " + row["NOTES"].ToString() + "<br>");
                    }
                }

                if (row["DOCTOR_ID"] != null && row["DOCTOR_ID"] != DBNull.Value)
                {
                    if (row["NOTES"] != null)
                    {
                        sb.Append("Plan: " + row["NOTES"].ToString() + "<br>");
                    }
                }
            }




        if (dsPatVital != null && dsPatVital.Tables.Count > 0)
        {
            DataRow row = dsPatVital.Tables[0].Rows[0];

            sb.Append("<h3>Patient Vital Signs</h3>");
            //sb.Append("Chief Complain:" + row["CHIEF_COMPLAINT"].ToString() + "<br>");
            sb.Append("Temperature:" + row["TEMPERATURE"].ToString() + "°" + row["TEMPERATURE_UNIT"].ToString() + "<br>");
            sb.Append("Blood Pressure:" + row["BLOOD_PRESSURE_SYSTOLIC"].ToString() + "(systolic) " + row["BLOOD_PRESSURE_DIASTOLIC"].ToString() + "(diastolic)" + "<br>");
            sb.Append("Pulse:" + row["PULSE"].ToString() + " beats per minute " + row["PULSE_TYPE"].ToString() + "<br>");
            sb.Append("Respiratory Rate:" + row["RESPIRATORY_RATE"].ToString() + " beats per minute " + "<br>");
            sb.Append("BMI:" + "Weight:" + row["BMI_WEIGHT"].ToString() + " , Height:" + row["BMI_HEIGHT"].ToString() + "<br>");
        }



        //DataSet dsMPIInfo = PatientManager.GetMPIInfo(patientKey);
        //if (dsMPIInfo != null && dsMPIInfo.Tables.Count > 0)
        //{
        //    sb.Append("<h3>Master Patient Code Index</h3>");
        //    sb.Append("<table>");
        //    sb.Append("<thead>");
        //    sb.Append("<tr><th>Code</th><th>Type</th><th>Version</th><th>Medical Content Index</th><th>Service Date</th><th>Time(EST)</th><th>Provider ID</th><th>Healthcare Facility ID</th></tr>");
        //    sb.Append("</thead>");
        //    sb.Append("<tbody>");
        //    for (int i = 0; i < dsMPIInfo.Tables[0].Rows.Count; i++)
        //    {
        //        DataRow row = dsMPIInfo.Tables[0].Rows[i];
        //        sb.Append("<tr>");
        //        sb.Append("<td>" + row["Code"] + "</td>");
        //        //sb.Append("<td>" + row["Modifier"] + "</td>");
        //        sb.Append("<td>" + row["Type"] + "</td>");
        //        sb.Append("<td>" + row["Version"] + "</td>");
        //        sb.Append("<td>" + row["MedicalContentIndex"] + "</td>");
        //        sb.Append("<td>" + row["DateOfService"] + "</td>");
        //        sb.Append("<td>" + row["ServiceTime"] + "</td>");
        //        sb.Append("<td>" + row["ProviderID"] + "</td>");
        //        sb.Append("<td>" + row["InstituteCode"] + "</td>");
        //        sb.Append("</tr>");
        //    }
        //    sb.Append("</tbody>");
        //    sb.Append("</table>");
        //}

        sb.Append("<a href=\"#\" target=\"_blank\">Acknowledge Receipt</a>");
        string mailBody = sb.ToString();

        string ErrorMessage= string.Empty;

        if (alertID.HasValue)
        {
            PatientManager.AddAlertEmail(alertID.Value, mailBody, email, out ErrorMessage); 
        }

        EmailManager client = new EmailManager(email, "Patient Report", true, mailBody);
        client.Send();
    }

    public static void SendEmailTo(long patientKey, long soaps_id, int recorded_vitalsigns_id,string email)
    {
        StringBuilder sb = new StringBuilder();

        DataSet dsPatDemo = PatientManager.GetPatientDemographics(patientKey, HttpContext.Current.User.Identity.Name);

        if (dsPatDemo != null && dsPatDemo.Tables.Count > 0)
        {
            DataRow row = dsPatDemo.Tables[0].Rows[0];

            sb.Append("<h3>Patient Demographic Information</h3>");

            sb.Append((String.IsNullOrEmpty(row["PatientID"].ToString()) ? "" : "Patient Id:" + row["PatientID"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Prefix"].ToString()) ? "" : "Prefix: " + row["Prefix"].ToString() + "<br>"));

            sb.Append(String.IsNullOrEmpty(row["FirstName"].ToString()) ? "" : "First Name:" + row["FirstName"].ToString() + "<br>");
            sb.Append((String.IsNullOrEmpty(row["MiddleName"].ToString()) ? "" : "Middle Name:" + row["MiddleName"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["LastName"].ToString()) ? "" : "Last Name:" + row["LastName"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Suffix"].ToString()) ? "" : "Suffix:" + row["Suffix"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["StreetAddress"].ToString()) ? "" : "Street Address:" + row["StreetAddress"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["CityTown"].ToString()) ? "" : "City/Town:" + row["CityTown"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Country"].ToString()) ? "" : "County: " + row["Country"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["State"].ToString()) ? "" : "State: " + row["State"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Zip"].ToString()) ? "" : "Zip Code:" + row["Zip"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["County"].ToString()) ? "" : "Country:" + row["County"].ToString() + "<br>"));


            sb.Append((row["HomePhone"] == DBNull.Value) ? "Home Phone:" + "N/A<br>" : "Home Phone:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["HomePhone"])) + "<br>");
            sb.Append((row["BusinessPhone"] == DBNull.Value) ? "Business Phone:" + "N/A<br>" : "Business Phone:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["BusinessPhone"])) + "<br>");
            sb.Append((row["CellPhone"] == DBNull.Value) ? "Mobile Phone:" + "N/A<br>" : "Mobile Phone:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["CellPhone"])) + "<br>");
            sb.Append((row["Pager"] == DBNull.Value) ? "Pager Number:" + "N/A<br>" : "Pager Number:" + String.Format("{0:(###) ###-####}", Double.Parse((string)row["Pager"])) + "<br>");


            sb.Append((String.IsNullOrEmpty(row["PhysicalMarks"].ToString()) ? "" : "Physical Marks:" + row["PhysicalMarks"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["DateOfBirth"].ToString()) ? "" : "Date of Birth:" + row["DateOfBirth"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["BirthPlace"].ToString()) ? "" : "Birth Place:" + row["BirthPlace"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Sex"].ToString()) ? "" : "Sex:" + row["Sex"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["MaritalStatus"].ToString()) ? "" : "Marital Status:" + row["MaritalStatus"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["LanguagesSpoken"].ToString()) ? "" : "Languages Spoken:" + row["LanguagesSpoken"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Religion"].ToString()) ? "" : "Religion:" + row["Religion"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["BloodType"].ToString()) ? "" : "Blood Type:" + row["BloodType"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Occupation"].ToString()) ? "" : "Occupation:" + row["Occupation"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["HairColor"].ToString()) ? "" : "Hair Color:" + row["HairColor"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["EyeColor"].ToString()) ? "" : "Eye Color:" + row["EyeColor"].ToString() + "<br>"));
            sb.Append((String.IsNullOrEmpty(row["Race"].ToString()) ? "" : "Race:" + row["Race"].ToString() + "<br>"));

        }

        DataSet dsPatVital = PatientManager.GetPatientVitalSignsByID(recorded_vitalsigns_id);

        if (dsPatVital != null && dsPatVital.Tables.Count > 0)
        {
            DataRow row = dsPatVital.Tables[0].Rows[0];


            sb.Append("<h3>Patient Vital Signs</h3>");
            sb.Append("Chief Complain:" + row["CHIEF_COMPLAINT"].ToString() + "<br>");
            sb.Append("Temperature:" + row["TEMPERATURE"].ToString() + "°" + row["TEMPERATURE_UNIT"].ToString() + "<br>");
            sb.Append("Blood Pressure:" + row["BLOOD_PRESSURE_SYSTOLIC"].ToString() + "(systolic) " + row["BLOOD_PRESSURE_DIASTOLIC"].ToString() + "(diastolic)" + "<br>");
            sb.Append("Pulse:" + row["PULSE"].ToString() + " beats per minute " + row["PULSE_TYPE"].ToString() + "<br>");
            sb.Append("Respiratory Rate:" + row["RESPIRATORY_RATE"].ToString() + " beats per minute " + "<br>");
            sb.Append("BMI:" + "Weight:" + row["BMI_WEIGHT"].ToString() + " , Height:" + row["BMI_HEIGHT"].ToString() + "<br>");
        }

        DataSet dsMPIInfo = PatientManager.GetMPIInfo(patientKey);
        if (dsMPIInfo != null && dsMPIInfo.Tables.Count > 0)
        {
            sb.Append("<h3>Master Patient Code Index</h3>");
            sb.Append("<table>");
            sb.Append("<thead>");
            sb.Append("<tr><th>Code</th><th>Type</th><th>Version</th><th>Medical Content Index</th><th>Service Date</th><th>Time(EST)</th><th>Provider ID</th><th>Healthcare Facility ID</th></tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            for (int i = 0; i < dsMPIInfo.Tables[0].Rows.Count; i++)
            {
                DataRow row = dsMPIInfo.Tables[0].Rows[i];
                sb.Append("<tr>");
                sb.Append("<td>" + row["Code"] + "</td>");
                //sb.Append("<td>" + row["Modifier"] + "</td>");
                sb.Append("<td>" + row["Type"] + "</td>");
                sb.Append("<td>" + row["Version"] + "</td>");
                sb.Append("<td>" + row["MedicalContentIndex"] + "</td>");
                sb.Append("<td>" + row["DateOfService"] + "</td>");
                sb.Append("<td>" + row["ServiceTime"] + "</td>");
                sb.Append("<td>" + row["ProviderID"] + "</td>");
                sb.Append("<td>" + row["InstituteCode"] + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
        }


        sb.Append("<a href=\"#\" target=\"_blank\">Acknowledge Receipt</a>");
        string mailBody = sb.ToString();
        EmailManager client = new EmailManager(email, "Patient Report", true, mailBody);
        client.Send();
    }

    public static DataSet GetSMSReportHTMLById(int reportId)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetSMSReportHTMLById(reportId);
    }

    public static DataSet GET_UMR_PROVIDER_BY_NAME(string name_part)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GET_UMR_PROVIDER_BY_NAME(name_part);
    }


    public static DataSet GET_NURSE_BY_ID(string nurse_id)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GET_NURSE_BY_ID(nurse_id);
    }

    #region GET DIFFERENT CODES
    public static DataSet GetAllDiagnosticTest(string medcode)
    {
        DataAccess dataAccess = new DataAccess();
        return dataAccess.GetAllDiagnosticTest(medcode);
    }
    public static DataSet GetRadiology(string medcode)
    {
        DataAccess dataAccess = new DataAccess();
        return dataAccess.GetRadiology(medcode);
    }
    public static DataSet GetPathologyLaboratory(string medcode)
    {
        DataAccess dataAccess = new DataAccess();
        return dataAccess.GetPathologyLaboratory(medcode);
    }
    public static DataSet GetProceduresOthers(string medcode)
    {
        DataAccess dataAccess = new DataAccess();
        return dataAccess.GetProceduresOthers(medcode);
    }
    public static DataSet GetPerformanceMeasurements(string medcode)
    {
        DataAccess dataAccess = new DataAccess();
        return dataAccess.GetPerformanceMeasurements(medcode);
    }
    public static DataSet GetEmergingTechServices(string medcode)
    {
        DataAccess dataAccess = new DataAccess();
        return dataAccess.GetEmergingTechServices(medcode);
    }
    
    #endregion //end GET DIFFERENT CODES
    #endregion

    #region Nurse Related
    public static DataSet GetNurses()
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetNurses();
    }

    public static int GetNurseCount(string ID, string FName, string LName)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetNurseCount(ID, FName, LName);
    }
    public static DataTable GetNurseList(string ID, string FName, string LName, int currentPageIndex, int pageSize, string orderBy)
    {
        if (FName != null)
        {
            FName = FName.Replace("'", "''");
        }
        if (LName != null)
        {
            LName = LName.Replace("'", "''");
        }
        DataAccess DAL = new DataAccess();
        DataSet ds = DAL.GetNurseList(ID, FName, LName, currentPageIndex, pageSize, orderBy);
        return ds.Tables.Count != 0 ? ds.Tables[0] : new DataTable();
    }
    public static DataTable GetNurseSearchPageData(string query, int currentPage, int pageSize, int option)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetNurseSearchPageData(query, currentPage, pageSize, option);
    }
    public static int GetNurseSearchPageCount(string query, int pageSize, int option)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetNurseSearchPageCount(query, pageSize, option);
    }
    public static int GetNurseSearchRecordCount(string query, int option)
    {
        DataAccess DAL = new DataAccess();
        return DAL.GetNurseSearchRecordCount(query, option);
    }
    #endregion
}
