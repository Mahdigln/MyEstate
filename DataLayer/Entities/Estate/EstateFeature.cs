namespace DataLayer.Entities.Estate;

public class EstateFeature
{
    public int EstateFeatureId { get; set; }
    public string FeatureTitle { get; set; }

    public int EstateId { get; set; }

    #region Relations

    public Estate Estate { get; set; }
    

    #endregion

}