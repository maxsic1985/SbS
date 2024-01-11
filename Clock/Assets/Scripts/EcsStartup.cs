using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;


namespace MSuhinin.Clock
{
    public sealed class EcsStartup : MonoBehaviour
    {
        public EcsSystems Systems { get; private set; }
        private bool _hasInitCompleted;
        
        const string API_URL = "http://worldtimeapi.org/api/ip";
        const string NTP_URL = "ntp.ix.ru";
        
        [SerializeField] EcsUguiEmitter uguiEmitter;

        
        private async void Start()
        {
            Application.targetFrameRate = 60;

            var world = new EcsWorld();
            Systems = new EcsSystems(world);
            
            // IWorldTimeService worldTime = new WorldTimeServiceFromApi();
            // worldTime.Initialize(API_URL);
            
            IWorldTimeService ntpTime = new NtpTimeService();
            ntpTime.Initialize(NTP_URL);
          
            new InitializeAllSystem(Systems);
            
            
            Systems
                .AddWorld (new EcsWorld (), WorldsNamesConstants.EVENTS)
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(WorldsNamesConstants.EVENTS))

#endif
               
                .InjectUgui (uguiEmitter, WorldsNamesConstants.EVENTS)
                .Init();
            _hasInitCompleted = true;
        }

        private void Update()
        {
            if (_hasInitCompleted)
                Systems?.Run();
        }

        private void OnDestroy()
        {
            if (Systems != null)
            {
                foreach (var worlds in  Systems.GetAllNamedWorlds())
                {
                    worlds.Value.Destroy();
                }
                Systems.GetWorld().Destroy();
                Systems = null;
            }
        }
    }
}