using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SignalHandler<T>(T signal) where T : ISignal;

public interface ISignal
{
}

public static class SignalSystem<T> where T : ISignal
{
    private static event SignalHandler<T> OnSignal = delegate { };

    public static void Pub(T message)
    {
        /*var debugStr = Newtonsoft.Json.JsonConvert.SerializeObject(message);
        UnityEngine.Debug.LogFormat("Pub: {0}: {1}", typeof(T), debugStr);*/
        OnSignal(message);
    }

    public static void Sub(SignalHandler<T> handler)
    {
        OnSignal += handler;
    }

    public static void UnSub(SignalHandler<T> handler)
    {
        OnSignal -= handler;
    }
}