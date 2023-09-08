namespace DataLayer.Entities.Estate;

public class EstateType
{
    public int EstateTypeId { get; set; }
    public string EstateTypeTitle { get; set; }

    #region Relations

    public List<Estate> Estates { get; set; }

    #endregion
}