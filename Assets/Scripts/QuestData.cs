using System.Collections;
using System.Collections.Generic;

public class QuestData
{
    // 유니티 필요없음.
    public string questName;
    public int[] npcId;

    // 구조체 생성을 위해 매개 변수 생성자 작성
    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
