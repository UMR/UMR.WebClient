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
/// Summary description for ProviderList
/// </summary>
public class Provider
{
    private string providerID;

    public string ProviderID
    {
        get { return providerID; }
        set { providerID = value; }
    }
    private string lastName;

    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }
    private string firstName;

    public string FirstName
    {
        get { return firstName; }
        set { firstName = value; }
    }
    private string phone;

    public string Phone
    {
        get { return phone; }
        set { phone = value; }
    }
    private string fax;

    public string Fax
    {
        get { return fax; }
        set { fax = value; }
    }
    private string cellPhone;

    public string CellPhone
    {
        get { return cellPhone; }
        set { cellPhone = value; }
    }
    private string pager;

    public string Pager
    {
        get { return pager; }
        set { pager = value; }
    }
    private string institutionID;

    public string InstitutionID
    {
        get { return institutionID; }
        set { institutionID = value; }
    }

    private string instituitionName;

    public string InstituitionName
    {
        get { return instituitionName; }
        set { instituitionName = value; }
    }

    private int disciplineCode;

    public int DisciplineCode
    {
        get { return disciplineCode; }
        set { disciplineCode = value; }
    }

    private string disciplineDescription;

    public string Discipline
    {
        get { return disciplineDescription; }
        set { disciplineDescription = value; }
    }

    private string webSite;

    public string WebSite
    {
        get { return webSite; }
        set { webSite = value; }
    }
    private string eMail;

    public string EMail
    {
        get { return eMail; }
        set { eMail = value; }
    }

	public Provider()
	{
	}


}
