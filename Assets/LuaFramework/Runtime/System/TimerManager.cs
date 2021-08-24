using System;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace LuaFramework
{
    public class TimerItem
    {
        public uint id;
        public int times;
        public float timeout;
        public float nextTime;
        public Action action;
    }
    public class TimerManager : GameFrameworkComponent
    {
        uint id_;
        public Dictionary<uint, TimerItem> timers=new Dictionary<uint, TimerItem>();
        List<uint> needRemove = new List<uint>();
        private void Start()
        {
            id_ = 1;
            timers.Clear();
        }

        private void Update()
        {
            needRemove.Clear();
            foreach (var item in timers.Values)
            {
                if(Time.time > item.nextTime)
                {
                    OnTimeOut(item);
                }
            }

            foreach(var id in needRemove)
            {
                timers.Remove(id);
            }
        }
        public void OnTimeOut(TimerItem item)
        {
            item.times--;
            item.action();
            if (item.times == 0)
            {
                needRemove.Add(item.id);
            }
            else
            {
                item.nextTime = Time.time + item.timeout;
            }
        }
        public bool IsRuning(uint id)
        {
            return timers.ContainsKey(id);
        }

        public bool StopTimer(uint id)
        {
            return timers.Remove(id);
        }

        public uint Delay(float timeout, Action action)
        {
            return AddTimer(1, timeout, action);
        }

        public uint Loop(float timeout, Action action)
        {
            return AddTimer(-1, timeout, action);
        }

        public uint AddTimer(int times, float timeout, Action action) {
            if (times == 0)
            {
                Debug.LogError("Timer.Times: times must above 0");
                return 0;
            }
            if (timeout <= 0)
            {
                Debug.LogError("Timer.Times: delay must above 0");
                return 0;
            }
            var id = id_++;
            var item = new TimerItem();
            item.id = id;
            item.times = times;
            item.timeout = timeout;
            item.action = action;
            item.nextTime = Time.time + timeout;
            timers.Add(id, item);
            return id;
        }
    }
}
