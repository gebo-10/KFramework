SysInfo = SysInfo  or Singleton("SysInfo")

function SysInfo:__init()
	
end

function SysInfo:PrintInfo( )
    log("物理地址:",LuaHelper.GetMacAddress())
    log("电量：",UnityEngine.SystemInfo.batteryLevel)
    log("设备模型：" , UnityEngine.SystemInfo.deviceModel)
    log("设备名称：" , UnityEngine.SystemInfo.deviceName)
    log("设备类型：" , UnityEngine.SystemInfo.deviceType)
    log("设备唯一标识符：" ,UnityEngine.SystemInfo.deviceUniqueIdentifier)
    log("是否支持纹理复制：" , UnityEngine.SystemInfo.copyTextureSupport)
    log("显卡ID：" , UnityEngine.SystemInfo.graphicsDeviceID)
    log("显卡名称：" , UnityEngine.SystemInfo.graphicsDeviceName)
    log("显卡类型：" , UnityEngine.SystemInfo.graphicsDeviceType)
    log("显卡供应商：" , UnityEngine.SystemInfo.graphicsDeviceVendor)
    log("显卡供应商ID：" , UnityEngine.SystemInfo.graphicsDeviceVendorID)
    log( "显卡版本号：" , UnityEngine.SystemInfo.graphicsDeviceVersion)
    log( "显存大小（单位：MB）：" , UnityEngine.SystemInfo.graphicsMemorySize)
    log( "是否支持多线程渲染：" , UnityEngine.SystemInfo.graphicsMultiThreaded)
    log( "支持的渲染目标数量：" , UnityEngine.SystemInfo.supportedRenderTargetCount) 
    log( "系统内存大小（单位：MB）：" , UnityEngine.SystemInfo.systemMemorySize)
    log( "操作系统：" , UnityEngine.SystemInfo.operatingSystem)
end

function SysInfo:GetUUID()
    local mac=LuaHelper.GetMacAddress()
    local device_id=UnityEngine.SystemInfo.graphicsDeviceID
    local id=UnityEngine.SystemInfo.deviceUniqueIdentifier
    return mac..device_id..id
end