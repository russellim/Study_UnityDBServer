# Space Shooter :rocket:
<br>

## 기본환경
- unity ver: 2019.4.7f
- Firebase Database 6.15.2
- PHP7.3, Apach2.2, MySQL5.7

<br>

## 간략기능
- Player has level, exp and hp, Status
- Player Attack Status
- Player special attack
- 3 Types Enemy
- Score db(firebase)
- Items(Heart, PowerUp, Special)
- Setting(sound on/off, handed)

<br>

## 주요기능상세
- Firebase Database vs PHPServer

스코어 저장/불러오기. 플레이어닉네임(게임오버시 입력)/레벨/스코어/게임시간을 저장.
<div>
  <img width="300" src = "https://user-images.githubusercontent.com/38000693/92399193-d7eeda80-f164-11ea-8943-ee94685445fc.PNG">Firebase Database
</div>

적용이 간단하다 (메뉴얼만 따라하면 ok), 유니티 내의 코드만 있으면 된다.  
데이터를 테이블이 아닌 Json구조로 볼 수 있다. 테이블로 확인해야하는 데이터라면 적절하지 않을수도.  
그 대신 확장성은 자유자재.
  
<br>
<div>
  <img width="500" src = "https://user-images.githubusercontent.com/38000693/92399189-d6bdad80-f164-11ea-9d13-9ad74854c754.PNG">PHPmyadmin
</div>

사용하기 위해 준비해야 할 것이 많다. 사용할 쿼리를 .php 파일을 따로 작성해서 디렉토리에 넣어줘야한다.  
게임 내에서 텍스트를 입력받아 저장하는 경우, 보안에 문제가 생길 수 있다. (테이블 통째로 날려버리기 같은 심각한 문제)
<br>
<br>
랭킹 창
<div>
  <img width="300" src = "https://user-images.githubusercontent.com/38000693/90873095-88748480-e3d8-11ea-98d9-a1eefbb0fc3f.jpg">
  <img width="300" src = "https://user-images.githubusercontent.com/38000693/90873088-86122a80-e3d8-11ea-8f0e-fc6f4bacbadf.jpg">
  <img width="300" src = "https://user-images.githubusercontent.com/38000693/90873089-86aac100-e3d8-11ea-970d-e8b27ada8f94.jpg">
</div>
<br>
<br>

- json 파싱

플레이어 레벨에 따른 능력치 표를 불러오는데 사용.
```c
{
  "PlayerLevel": [
    {
      "Level": 1,
      "Exp": 5,
      "EnemySpeed": 0
    },
    ... ] }
```
<br>

- Object Pooling

오브젝트 Instatiate와 Destroy시의 가비지 콜렉터(GC)를 줄이기 위해 미리 오브젝트를 만들고 활성/비활성으로 재사용. 게임 내내 생성되는 총알, 적 기체, 폭발 이펙트 등을 미리 풀링함.

<br>
<br>

- Audio Mixer

Groups 목록(BGM, SFX, UI Sound)생성. Audio Source 컴포넌트의 Output에 연결하여 사운드 옵션 조정.
<div>
  <img width="500" src = "https://user-images.githubusercontent.com/38000693/90873098-890d1b00-e3d8-11ea-987b-a981915db176.jpg">
</div>

<br>
<br>

## 스크린샷

<div>
  <img width="300" src = "https://user-images.githubusercontent.com/38000693/90873063-7db9ef80-e3d8-11ea-9c41-291cf138f9b3.jpg">
  <img width="300" src = "https://user-images.githubusercontent.com/38000693/90873085-84e0fd80-e3d8-11ea-8c4f-41446331f902.jpg">
  <img width="300" src = "https://user-images.githubusercontent.com/38000693/90873086-85799400-e3d8-11ea-9fc2-2103004d8a20.jpg">
  <img width="300" src = "https://user-images.githubusercontent.com/38000693/90873091-87dbee00-e3d8-11ea-8251-410f49e9aba4.jpg">
  <img width="300" src = "https://user-images.githubusercontent.com/38000693/90873079-83afd080-e3d8-11ea-9c5d-5477b62eac43.jpg">
  <img width="300" src = "https://user-images.githubusercontent.com/38000693/90873077-827ea380-e3d8-11ea-916c-cd5df62f0edb.jpg">
</div>


