using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public saveManager save;
    // 갯수
    public int ML_count; // 우유
    public int Hot_count; // 온도올리기
    public int MTs_count; // 고기소
    public int FR_count; // 과일
    public int MTl_count; //고기대
    public int FS_count; // 물고기
    public int SR_count; // 새우
    public int InS_count; // 벌레
    public int NT_count; // 나무열매 
    public int LZ_count; // 도마뱀
    public int ER_count; // 지렁이

    public int revivalPortion; // 부활물약
    public int bloodPortion;// 체력물약
    public int speedPortion; // 속도물약
    public int FemalePortion;// 암컷촉진물약
    public int MalePortion; // 수컷촉진물약

    // 경험치
    public int ML_EP = 1; // 우유
    public int Hot_EP = 1; // 온도올리기
    public int MTs_EP= 2; // 고기소
    public int FR_EP = 3; // 과일
    public int MTl_EP = 2; //고기대
    public int FS_EP = 3; // 물고기
    public int SR_EP = 3; // 새우
    public int InS_EP = 4; // 벌레
    public int NT_EP = 4; // 나무열매 
    public int LZ_EP = 3; // 도마뱀
    public int ER_EP = 4; // 지렁이

    void OnEnable()
    {
        ML_count = save.sML_count;
        Hot_count = save.sHot_count;
        FR_count = save.sFR_count;
        FS_count = save.sFS_count;
        MTl_count = save.sMTl_count;
        MTs_count = save.sMTs_count;
        SR_count = save.sSR_count;
        InS_count = save.sInS_count;
        NT_count = save.sNT_count;
        LZ_count = save.sLZ_count;
        ER_count = save.sER_count;
        revivalPortion = save.sRevivalPortion;
        bloodPortion = save.sBloodPortion;
        speedPortion = save.sSpeedPortion;
        FemalePortion = save.sFemalePortion;
        MalePortion = save.sMalePortion;
    }
}
