using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaiNull
{
    public class RoomManager : Singleton<RoomManager>
    {
        [SerializeField] private RoomData startRoomData;
        
        public static Room CurrentRoom { get; private set; }
        
        public static event Action<Room> OnCurrentRoomUpdate;
        
        public RoomData Data
        {
            get => startRoomData;
            set => startRoomData = value;
        }

        public override void Awake()
        {
            if (startRoomData) {
                _ = LoadRoom(startRoomData);
            }
        }
        
        public static async Awaitable LoadRoom(RoomData roomData)
        {
            // TODO: MultiThreading??
            Debug.Log($"Started Loading {roomData.sceneName}");
            // await Awaitable.BackgroundThreadAsync();
            await Awaitable.FromAsyncOperation(SceneManager.LoadSceneAsync(roomData.sceneName, LoadSceneMode.Additive) ?? throw new InvalidOperationException(), Instance.destroyCancellationToken);
            // await Awaitable.MainThreadAsync();
            GameObject[] gameObjects = SceneManager.GetSceneByName(roomData.sceneName).GetRootGameObjects();

            Debug.Log($"Loaded {roomData.sceneName}");
            
            if (gameObjects != null) {
                foreach (GameObject go in gameObjects) {
                    if (go.TryGetComponent(out Room room)) {
                        CurrentRoom = room;
                        OnCurrentRoomUpdate?.Invoke(room);
                    }
                }
            }
        }
    }
}
