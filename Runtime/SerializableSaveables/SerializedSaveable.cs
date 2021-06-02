namespace DaBois.Saving
{
    public interface SerializedSaveable<T>
    {
        byte[] Serialize();
        T Deserialize(byte[] data);
    }
}