using System;
using UnityEngine;

// RTDB의 users/{uid} 에 저장되는 유저 프로필 데이터.
// 이름 충돌 주의: Firebase.Auth.UserProfile(프로필 갱신용)과 구분하기 위해 UserProfileData로 명명.
[Serializable]
public class UserProfileData
{
    public string nickname;
    public string email;
    public long createdAt;

    public UserProfileData() { }

    public UserProfileData(string nickname, string email)
    {
        this.nickname = nickname;
        this.email = email;
        this.createdAt = TimeUtil.NowUnixMillis();
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public static UserProfileData FromJson(string json)
    {
        return JsonUtility.FromJson<UserProfileData>(json);
    }
}
