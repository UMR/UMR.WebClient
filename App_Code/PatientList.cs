using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for PatientList
/// </summary>
public class Patients
{
    
    private string _id;
    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }
    
    private string _modifierID;
    public string ModifierID
    {
        get { return _modifierID; }
        set { _modifierID = value; }
    }
    
    private string _firstName;
    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }
    
    private string _lastName;
    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    private string _dateOfBirth;
    public string DateOfBirth
    {
        get { return _dateOfBirth; }
        set { _dateOfBirth = value; }
    }


	public Patients()
	{
		//
		// TODO: Add constructor logic here
		//
	}

}
