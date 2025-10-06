using UnityEngine;

public class SocketDriver : MonoBehaviour
{
    [SerializeField] private Socket socket;

    public int GetSocketStatus()
    {
        return socket.currentIndex;
    }

    public void SetSocketStatus(int index)
    {
        socket.currentIndex = index;
    }

    public void OpenSocket()
    {
        socket.currentIndex -= 1;
    }

    public void CloseSocket()
    {
        socket.currentIndex += 1;
    }
}
