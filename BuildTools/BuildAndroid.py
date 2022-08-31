import sys
import os
import fileinput
import subprocess
 
#Jenkins配置教程：http://www.u3d8.com/?p=1996
 
if __name__ == '__main__':
    
    #Jenkins传递 无需手动修改
    jenkinsParams = {
        'WorkSpace':os.environ['WorkSpace'].strip().replace('\\', '/'),
        'BuildVersion':os.environ['BuildVersion'].strip(), 
        'VersionCode':os.environ['VersionCode'].strip(),
        'IsDevelopment':os.environ['IsDevelopment'].strip(),
        'BUILD_NUMBER':os.environ['BUILD_NUMBER'].strip(),
    } 
 
    #本地参数 需手动修改
    localParams = {
        'UnityPath':'C:/Program Files/Unity/Hub/Editor/2019.4.23f1c1/Editor/Unity.exe',
        'ProjectPath':jenkinsParams['WorkSpace'] + '/Client',
        'LogPath':jenkinsParams['WorkSpace'] + '/Client/BuildTools/OutPut/apk_log.txt',
    }
    
    print('Jenkins参数')
    print(jenkinsParams)
    print('本地参数')
    print(localParams)
    
    print('开始构建');
    
    #调用BuildEditor.BuildAPK方法 编译APK    
    cmd = '"' + localParams['UnityPath'] + '" -batchmode -projectPath "' + localParams['ProjectPath'] + '" -nographics -executeMethod BuildEditor.BuildAPK -logFile "' + localParams['LogPath'] + '" -quit ' + jenkinsParams['BuildVersion'] + ' ' + jenkinsParams['VersionCode'] + ' ' + jenkinsParams['IsDevelopment'] + ' ' + jenkinsParams['BUILD_NUMBER'];
    print(cmd);    
    subprocess.call(cmd);
        
    #在Jenkins中打印Log
    fileHandler = open(localParams['LogPath'], mode='r', encoding='UTF-8')
    report_lines = fileHandler.readlines()
    for line in report_lines:
        print(line.rstrip())
        
    print('结束构建')