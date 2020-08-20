# Space Shooter :rocket:

업데이트 중!


## 기본환경
- unity ver: 2019.4.7f
- use sdk: Firebase Database 6.15.2 (점수 랭킹 저장, 불러오기)


## 기능
- Player has level, exp and hp, Status
- Player Attack Status
- Player special attack
- 3 Types Enemy
- Score db(firebase)
- Items(Heart, PowerUp, Special)
- Setting(sound on/off, handed)


## 주요기능상세
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

- Object Pooling

오브젝트 Instatiate와 Destroy시의 가비지 콜렉터(GC)를 줄이기 위해 미리 오브젝트를 만들고 활성/비활성으로 재사용. 게임 내내 생성되는 총알, 적 기체, 폭발 이펙트 등을 미리 풀링함.


- Firebase Database

스코어 저장/불러오기. 플레이어닉네임(게임오버시 입력)/레벨/스코어/게임시간을 저장.


- Audio Mixer

Groups 목록(BGM, SFX, UI Sound)생성. Audio Source 컴포넌트의 Output에 연결하여 사운드 옵션 조정.
