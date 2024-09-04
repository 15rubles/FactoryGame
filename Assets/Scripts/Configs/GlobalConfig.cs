public static class GlobalConfig
{
    private static float speed = 2;

    public static float GetSpeed() {
        return speed;
    }

    public static void SetSpeed(float speed) {
        GlobalConfig.speed = speed;
    }

    public static void IncreaseSpeed(float valueToAdd) {
        speed += valueToAdd;
    }
}
