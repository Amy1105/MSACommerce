namespace CommonServiceClient
{
    public interface ILoadBalancer
    {
        string GetNode(List<string> nodes);
    }

}
