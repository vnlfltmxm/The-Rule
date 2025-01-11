public interface IPoolable
{
    void OnPoolReturn(); // 풀에 반환될 때 호출
    void OnPoolInitialize(); // 풀에서 빌려올 때 초기화
}