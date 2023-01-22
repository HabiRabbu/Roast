using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeDesignManager : MonoBehaviour
{

    public List<CoffeeDesign> coffeeDesigns;
    public List<CoffeeTypeSO> coffeeTypes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CoffeeDesign CreateBean(string designName, CoffeeTypeSO coffeeType)
    {
        BeanProduct beanProduct = new BeanProduct();
        beanProduct.coffeeType = coffeeType;

        CoffeeDesign beanDesign = new CoffeeDesign();
        beanDesign.Product = beanProduct;
        return beanDesign;
    }
    //public DriedProduct CreateDried()
    //{

    //}
    //public RoastedProduct CreateRoasted()
    //{

    //}
    //public GroundProduct CreateGround()
    //{

    //}
    //public OtherProduct CreateOther()
    //{

    //}

    public void AddCoffeeDesign(CoffeeDesign design)
    {
        coffeeDesigns.Add(design);
    }

    public void RemoveCoffeeDesign(CoffeeDesign design)
    {
        coffeeDesigns.Remove(design);
    }

    public List<CoffeeDesign> GetAllCoffeeDesigns()
    {
        return coffeeDesigns;
    }

}
