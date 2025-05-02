namespace WebAppDemo.Services;

public class SequenceService : ISequenceService
{
    private int _productId = 0;

    public int NextProductId()
    {
        return ++_productId;
    }
}
