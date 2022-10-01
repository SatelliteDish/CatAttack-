public sealed class Constants
{
    static Constants instance = null;
    static readonly object padlock = new object();

    Constants() {

    }

    public static Constants Instance {
        get {
            lock(padlock) {
                if(instance == null) {
                    return new Constants();
                }
                return instance;
            }
        }
    }
    public const string GroundTag = "Ground";
    public const string HazardTag = "Hazard";
    public const string PlayerTag = "Player";
}
