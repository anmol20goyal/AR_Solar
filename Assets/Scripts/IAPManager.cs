using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    private const string purchaseAll = "com.seraphic.solar.purchaseall";
    
    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == purchaseAll)
        {
            // unlock everything
            PlayerPrefs.SetInt("PurchasePlanets",1);
            // PlanetInfo.InstancePI.PurchaseSuccess();
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        if (product.definition.id == purchaseAll)
        {
            // purchase failed
            PlayerPrefs.SetInt("PurchasePlanets",0);
            Debug.Log("purchase failed");
        }
    }
}
