using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class TipReciever : MonoBehaviour
    {
        //Todo 언어

        public void ShowAutoSave() => Tip.Show(
            "AutoSave",
            "수정사항 발생 시 자동으로 수정사항을 저장합니다. 자동 저장 시에도 매번 백업 파일이 생성됩니다."
            );

        //Character
        public void ShowCharacterWeapons() => Tip.Show(
            "Weapons",
            "이 캐릭터가 가지는 무기입니다. 한 캐릭터가 여러개의 무기를 가질 수 있습니다.\n\nplayer 캐릭터에게 무기가 부여될 경우 시작할 때 기본적으로 보유하는 무기가 됩니다."
            );

        //Weapon
        public void ShowWeaponForPlayer() => Tip.Show(
            "For Player",
            "이 값이 체크 될 경우, 스테이지 보상에서 이 무기의 획득 및 업그레이드 선택지가 등장할 수 있게 됩니다."
            );

        public void ShowWeaponProjectileHp() => Tip.Show(
            "Hp",
            "Hp가 존재하는 투사체는 적의 공격을 받아 파괴될 수 있습니다.\n\n0으로 설정 시 파괴되지 않는 투사체가 됩니다."
            );

        public void ShowWeaponProjectileExplosionRange() => Tip.Show(
            "ExplosionRange",
            "타격 지점에서 이 값으로 설정 된 만큼의 폭발이 발생합니다. 폭발 효과는 1초간 유지되며, 그 동안 닿은 적 캐릭터 혹은 파괴 가능한 적 투사체를 제거합니다.\n\n만약 Hp가 설정되어 있다면, 공격받아 투사체가 제거될 때 폭발합니다.\n\n0으로 설정 시 폭발하지 않고 직접 닿은 대상에게만 피해를 입힙니다."
            );

        public void ShowWeaponProjectileHomming() => Tip.Show(
            "Homming",
            "가장 가까운 적 캐릭터를 향해 선회합니다. 이 값이 높을 수록 더 크게 선회합니다."
            );

        public void ShowWeaponProjectileLifetime() => Tip.Show(
            "Lifetime",
            "발사된 직후 부터 이 값 만큼의 초 동안 활성화 됩니다. 시간이 끝나면 투사체는 사라지며, Explosion Range가 설정 된 투사체의 경우 그 자리에서 폭발합니다."
            );

        public void ShowWeaponProjectileHitProjectile() => Tip.Show(
            "Hit Projectile",
            "이 값이 체크 될 경우 Hp를 보유한 투사체를 타격할 수 있게 됩니다."
            );

        public void ShowWeaponFireType() => Tip.Show(
            "Fire Type",
            "이 무기가 투사체를 어떤 방식으로 발사할지 결정합니다.\n\nFrontEvenSpread:\n전방을 향해 AngleRange 내 균등한 각도로 투사체들을 분산시켜 발사합니다.\n\nFrontArrow:\n전방을 향해 화살촉 형태로 투사체들을 정렬시켜 발사합니다. AngleRange의 영향을 받지 않으며, 대신 Scale의 영향을 받습니다.\n\nRandom:\nAngleRange 내 무작위한 방향으로 투사체를 발사합니다."
            );

        public void ShowWeaponFireCount() => Tip.Show(
            "Fire Count",
            "한 번에 발사되는 투사체의 숫자입니다."
            );

        public void ShowWeaponContinuousCount() => Tip.Show(
            "Continuous Count",
            "한 번에 연사하는 횟수입니다.\n\n각 사격은 고정적으로 0.1초의 간격을 가집니다."
            );

        public void ShowWeaponInterval() => Tip.Show(
            "Interval",
            "여기 입력 된 초마다 발사합니다."
            );

        public void ShowWeaponUpgrade() => Tip.Show(
            "Upgrade",
            "무기 각 업그레이드 단계에서 어떤 변화가 있는지를 편집합니다.\n입력한 값으로 덮어씌우는 방식으로 동작됩니다."
            );

        //Permanent Upgrade
        public void ShowPermanentUpgradeIncreaceHp() => Tip.Show(
            "Increase Hp Upgrade",
            "스테이지 클리어 보상으로 선택 가능한 항목 중 '최대 체력 증가' 업그레이드의 효과를 높입니다.\n\n스테이지 클리어 보상으로 '최대 체력 증가' 업그레이드를 선택 할 때마다 플레이어 캐릭터의 체력이 이 값 만큼 씩 추가로 증가합니다."
            );

        public void ShowPermanentUpgradeIncreaceMoveSpeed() => Tip.Show(
            "Increase Move Speed Upgrade",
            "스테이지 클리어 보상으로 선택 가능한 항목 중 '이동 속도 증가' 업그레이드의 효과를 높입니다.\n\n스테이지 클리어 보상으로 '이동 속도 증가' 업그레이드를 선택 할 때마다 플레이어 캐릭터의 이동 속도가 이 값 % 만큼 씩 추가로 증가합니다."
            );

        public void ShowPermanentUpgradeIncreaceCreditBonus() => Tip.Show(
            "Increase Move Speed Upgrade",
            "스테이지 클리어 보상으로 선택 가능한 항목 중 '크레딧 획득량 증가' 업그레이드의 효과를 높입니다.\n\n스테이지 클리어 보상으로 '크레딧 획득량 증가' 업그레이드를 선택 할 때마다 크레딧 획득량 증가량이 이 값 % 만큼 씩 추가로 증가합니다."
            );

        public void ShowPermanentUpgradeChoiceCount() => Tip.Show(
            "Increase Choice Count",
            "스테이지 클리어 보상으로 선택 가능한 선택지가 늘어납니다.\n\n기본 선택지 개수는 상단의 Stage Upgrade 메뉴에서 변경할 수 있습니다."
            );

        //Stage Upgrade
        public void ShowStageUpgradeChoiceCount() => Tip.Show(
            "Increase Choice Count",
            "스테이지 클리어 보상으로 선택 가능한 선택지 개수의 기본 값입니다.\n\nPermanent Upgrade(영구적인 업그레이드) 항목 중 선택지 개수를 높이는 업그레이드가 존재합니다."
            );

        public void ShowStageUpgradeHeal() => Tip.Show(
            "Heal",
            "체력을 지정 된 % 만큼 회복하는 선택지입니다."
            );

        public void ShowStageUpgradeCredit() => Tip.Show(
            "Credit",
            "고정 크레딧을 획득합니다. 모든 업그레이드가 소진되어 더 이상 등장할 수 있는 업그레이드가 없을 때에만 등장합니다."
            );

        public void ShowStageUpgradeWeight() => Tip.Show(
            "Weight",
            "등장 가중치입니다. 이 값에서 모든 등장 가능한 업그레이드의 가중치를 더한 값을 나눈 값이 이 선택지가 실제로 등장할 확률이 됩니다.\n\n각 업그레이드들이 소진 될 때마다 해당 업그레이드의 가중치가 사라지므로 확률에 변동이 발생할 수 있습니다."
            );
    }
}