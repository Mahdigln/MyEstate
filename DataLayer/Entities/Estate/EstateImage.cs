namespace DataLayer.Entities.Estate;

public class EstateImage
{
    public int EstateImageId { get; set; }
    public string EstateImageName { get; set; }

    public int EstateId { get; set; }

    #region Relations

    public Estate Estate { get; set; }


    #endregion
}