public static class RestaurantData
{
    private static float _money = 0f;

    public static float Money
    {
        get
        {
            return _money;
        }
        set
        {
            if (_money >= 0)
            {
                _money = value;
            }
        }
    }
    private static int _food = 0;

    public static int Food
    {
        get
        {
            return _food;
        }
        set
        {
            if( _food >= 0)
            {
                _food = value;
            }
        }
    }
    private static int _radiatedFood = 0;

    public static int RadiatedFood
    {
        get
        {
            return _radiatedFood;
        }
        set
        {
            if(_radiatedFood >= 0)
            {
                _radiatedFood = value;
            }
        }
    }
}
