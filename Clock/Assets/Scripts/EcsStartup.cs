using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;


namespace MSuhinin.Clock
{
    public sealed class EcsStartup : MonoBehaviour,IDragHandler
    {
        
        
        
        public EcsSystems Systems { get; private set; }
        private bool _hasInitCompleted;

     
        
        
        const string API_URL = "http://worldtimeapi.org/api/ip";
        const string NTP_URL = "ntp.ix.ru";

        [SerializeField] EcsUguiEmitter uguiEmitter;
        [SerializeField] bool NTP;

        private async void Start()
        {
            Application.targetFrameRate = 60;

            SharedData shared = new();
            await shared.Init();
            
            var world = new EcsWorld();
            Systems = new EcsSystems(world,shared);
            
            (IWorldTimeService,String) GetTimeService(bool timeService) => timeService switch
            {
                false => (new WorldTimeServiceFromApi(),API_URL),
                true => (new NtpTimeService(),NTP_URL),
            };


            var ts = GetTimeService(NTP);
            ts.Item1.Initialize(ts.Item2);

            new InitializeAllSystem(Systems);


            Systems
                .AddWorld(new EcsWorld(), WorldsNamesConstants.EVENTS)
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(WorldsNamesConstants.EVENTS))
#endif

                .InjectUgui(uguiEmitter, WorldsNamesConstants.EVENTS)
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
                foreach (var worlds in Systems.GetAllNamedWorlds())
                {
                    worlds.Value.Destroy();
                }

                Systems.GetWorld().Destroy();
                Systems = null;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_hasInitCompleted)
                Systems?.Run();
        }
    }
}