namespace ECS
{
    public interface ITempSensor
    {
        int GetTemp();

        bool RunSelfTest();
    }
}