using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class TipReceiver : MonoBehaviour
    {
        //Todo 언어

        public void ShowAutoSave() => Tip.Show(
            "자동 저장",
            "수정사항 발생 시 자동으로 수정사항을 저장합니다. 자동 저장 시에도 매번 백업 파일이 생성됩니다."
            );

        //Character
        public void ShowCharacterWeapons() => Tip.Show(
            "보유 무기",
            "이 캐릭터가 가지는 무기입니다. 한 캐릭터가 여러개의 무기를 가질 수 있습니다.\n\nplayer 캐릭터에게 무기가 부여될 경우 시작할 때 기본적으로 보유하는 무기가 됩니다."
            );

        public void ShowCharacterHomming() => Tip.Show(
            "선회력",
            "목표를 향해 선회하는 속도입니다.\n\n플레이어 캐릭터는 커서를, 적 캐릭터들은 플레이어 캐릭터를 향해 선회합니다."
            );

        //Weapon
        public void ShowWeaponForPlayer() => Tip.Show(
            "플레이어 전용",
            "이 값이 체크 될 경우, 게임 중 강화 선택지에서 이 무기의 획득 또는 강화가 등장할 수 있게 됩니다."
            );

        public void ShowWeaponProjectileHp() => Tip.Show(
            "체력",
            "체력이 존재하는 투사체는 적의 공격을 받아 파괴될 수 있습니다.\n\n0으로 설정 시 파괴되지 않는 투사체가 됩니다."
            );

        public void ShowWeaponProjectileExplosionRange() => Tip.Show(
            "폭발 범위",
            "타격 지점에서 이 값으로 설정 된 만큼의 폭발이 발생합니다. 폭발 효과는 1초간 유지되며, 그 동안 닿은 적 캐릭터 혹은 파괴 가능한 적 투사체를 제거합니다.\n\n만약 체력이 설정되어 있다면, 공격받아 투사체가 제거될 때 폭발합니다.\n\n0으로 설정 시 폭발하지 않고 직접 닿은 대상에게만 피해를 입힙니다."
            );

        public void ShowWeaponProjectileHomming() => Tip.Show(
            "선회력",
            "가장 가까운 적 캐릭터를 향해 선회합니다. 이 값이 높을 수록 더 크게 선회합니다."
            );

        public void ShowWeaponProjectileLifetime() => Tip.Show(
            "유지 시간",
            "발사된 직후 부터 이 값 만큼의 초 동안 활성화 됩니다. 시간이 끝나면 투사체는 사라지며, 폭발 범위가 설정 된 투사체의 경우 그 자리에서 폭발합니다."
            );

        public void ShowWeaponProjectileHitProjectile() => Tip.Show(
            "투사체 타격 가능",
            "이 값이 체크 될 경우 Hp를 보유한 적 투사체를 타격할 수 있게 됩니다."
            );

        public void ShowWeaponFireType() => Tip.Show(
            "사격 방식",
            "이 무기가 투사체를 어떤 방식으로 발사할지 결정합니다.\n\nFrontEvenSpread:\n전방을 향해 방사각 내 균등한 각도로 투사체들을 분산시켜 발사합니다.\n\nFrontArrow:\n전방을 향해 화살촉 형태로 투사체들을 정렬시켜 발사합니다. 방사각의 영향을 받습니다.\n\nRandom:\n방사각 내 무작위한 방향으로 투사체를 발사합니다."
            );

        public void ShowWeaponFireCount() => Tip.Show(
            "동시 발사 개수",
            "한 번에 발사되는 투사체의 숫자입니다."
            );

        public void ShowWeaponContinuousCount() => Tip.Show(
            "연사 횟수",
            "한 번에 연사하는 횟수입니다.\n\n각 사격은 고정적으로 0.1초의 간격을 가집니다."
            );

        public void ShowWeaponInterval() => Tip.Show(
            "공격 대기 시간",
            "여기 입력 된 초마다 발사합니다."
            );

        public void ShowWeaponUpgrade() => Tip.Show(
            "강화",
            "무기 각 강화 단계에서 어떤 변화가 있는지를 편집합니다.\n입력한 값으로 덮어씌우는 방식으로 동작됩니다."
            );

        //Permanent Upgrade
        public void ShowPermanentUpgradeIncreaceHp() => Tip.Show(
            "체력 증가",
            "player 캐릭터의 기본 체력에 더해집니다."
            );

        public void ShowPermanentUpgradeIncreaceDamage() => Tip.Show(
            "공격력 증가",
            "player 캐릭터가 발사하는 모든 투사체의 공격력이 증가합니다."
            );

        public void ShowPermanentUpgradeIncreaceMoveSpeed() => Tip.Show(
            "이동 속도 증가",
            "player 캐릭터의 기본 이동속도에 더해집니다."
            );

        public void ShowPermanentUpgradeIncreaceCreditBonus() => Tip.Show(
            "크레딧 보너스 증가",
            "기본 크레딧 획득량에 더해집니다."
            );

        public void ShowPermanentUpgradeChoiceCount() => Tip.Show(
            "강화 선택지 증가",
            "스테이지 클리어 보상으로 선택 가능한 선택지가 늘어납니다.\n\n기본 선택지 개수는 상단의 '게임 중 강화' 메뉴에서 변경할 수 있습니다."
            );

        //Stage Upgrade
        public void ShowStageUpgradeChoiceCount() => Tip.Show(
            "선택지 개수",
            "스테이지 클리어 보상으로 선택 가능한 선택지 개수의 기본 값입니다.\n\n영구적 강화 항목 중 선택지 개수를 높이는 강화가 존재합니다."
            );

        public void ShowStageUpgradeHeal() => Tip.Show(
            "회복",
            "체력을 지정 된 값 만큼 회복하는 선택지입니다.\n\n피해를 입은 상황에서만 등장합니다."
            );

        public void ShowStageUpgradeCredit() => Tip.Show(
            "크레딧",
            "고정 크레딧을 획득합니다. 모든 강화가 소진되어 더 이상 등장할 수 있는 강화가 없을 때에만 등장합니다.\n\n이 선택지로 제공되는 크레딧은 크레딧 획득량 증가 옵션을 실시간으로 적용 받습니다."
            );

        public void ShowStageUpgradeWeight() => Tip.Show(
            "가중치",
            "등장 가중치입니다. 이 값에서 모든 등장 가능한 강화의 가중치를 더한 값을 나눈 값이 이 선택지가 실제로 등장할 확률이 됩니다.\n\n각 강화들이 소진 될 때마다 해당 강화의 가중치가 사라지므로 확률에 변동이 발생할 수 있습니다."
            );
    }
}