using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class TipReciever : MonoBehaviour
    {
        //Todo ���

        public void ShowAutoSave() => Tip.Show(
            "AutoSave",
            "�������� �߻� �� �ڵ����� ���������� �����մϴ�. �ڵ� ���� �ÿ��� �Ź� ��� ������ �����˴ϴ�."
            );

        //Character
        public void ShowCharacterWeapons() => Tip.Show(
            "Weapons",
            "�� ĳ���Ͱ� ������ �����Դϴ�. �� ĳ���Ͱ� �������� ���⸦ ���� �� �ֽ��ϴ�.\n\nplayer ĳ���Ϳ��� ���Ⱑ �ο��� ��� ������ �� �⺻������ �����ϴ� ���Ⱑ �˴ϴ�."
            );

        //Weapon
        public void ShowWeaponForPlayer() => Tip.Show(
            "For Player",
            "�� ���� üũ �� ���, �������� ���󿡼� �� ������ ȹ�� �� ���׷��̵� �������� ������ �� �ְ� �˴ϴ�."
            );

        public void ShowWeaponProjectileHp() => Tip.Show(
            "Hp",
            "Hp�� �����ϴ� ����ü�� ���� ������ �޾� �ı��� �� �ֽ��ϴ�.\n\n0���� ���� �� �ı����� �ʴ� ����ü�� �˴ϴ�."
            );

        public void ShowWeaponProjectileExplosionRange() => Tip.Show(
            "ExplosionRange",
            "Ÿ�� �������� �� ������ ���� �� ��ŭ�� ������ �߻��մϴ�. ���� ȿ���� 1�ʰ� �����Ǹ�, �� ���� ���� �� ĳ���� Ȥ�� �ı� ������ �� ����ü�� �����մϴ�.\n\n���� Hp�� �����Ǿ� �ִٸ�, ���ݹ޾� ����ü�� ���ŵ� �� �����մϴ�.\n\n0���� ���� �� �������� �ʰ� ���� ���� ��󿡰Ը� ���ظ� �����ϴ�."
            );

        public void ShowWeaponProjectileHomming() => Tip.Show(
            "Homming",
            "���� ����� �� ĳ���͸� ���� ��ȸ�մϴ�. �� ���� ���� ���� �� ũ�� ��ȸ�մϴ�."
            );

        public void ShowWeaponProjectileLifetime() => Tip.Show(
            "Lifetime",
            "�߻�� ���� ���� �� �� ��ŭ�� �� ���� Ȱ��ȭ �˴ϴ�. �ð��� ������ ����ü�� �������, Explosion Range�� ���� �� ����ü�� ��� �� �ڸ����� �����մϴ�."
            );

        public void ShowWeaponProjectileHitProjectile() => Tip.Show(
            "Hit Projectile",
            "�� ���� üũ �� ��� Hp�� ������ ����ü�� Ÿ���� �� �ְ� �˴ϴ�."
            );

        public void ShowWeaponFireType() => Tip.Show(
            "Fire Type",
            "�� ���Ⱑ ����ü�� � ������� �߻����� �����մϴ�.\n\nFrontEvenSpread:\n������ ���� AngleRange �� �յ��� ������ ����ü���� �л���� �߻��մϴ�.\n\nFrontArrow:\n������ ���� ȭ���� ���·� ����ü���� ���Ľ��� �߻��մϴ�. AngleRange�� ������ ���� ������, ��� Scale�� ������ �޽��ϴ�.\n\nRandom:\nAngleRange �� �������� �������� ����ü�� �߻��մϴ�."
            );

        public void ShowWeaponFireCount() => Tip.Show(
            "Fire Count",
            "�� ���� �߻�Ǵ� ����ü�� �����Դϴ�."
            );

        public void ShowWeaponContinuousCount() => Tip.Show(
            "Continuous Count",
            "�� ���� �����ϴ� Ƚ���Դϴ�.\n\n�� ����� ���������� 0.1���� ������ �����ϴ�."
            );

        public void ShowWeaponInterval() => Tip.Show(
            "Interval",
            "���� �Է� �� �ʸ��� �߻��մϴ�."
            );

        public void ShowWeaponUpgrade() => Tip.Show(
            "Upgrade",
            "���� �� ���׷��̵� �ܰ迡�� � ��ȭ�� �ִ����� �����մϴ�.\n�Է��� ������ ������ ������� ���۵˴ϴ�."
            );

        //Permanent Upgrade
        public void ShowPermanentUpgradeIncreaceHp() => Tip.Show(
            "Increase Hp Upgrade",
            "�������� Ŭ���� �������� ���� ������ �׸� �� '�ִ� ü�� ����' ���׷��̵��� ȿ���� ���Դϴ�.\n\n�������� Ŭ���� �������� '�ִ� ü�� ����' ���׷��̵带 ���� �� ������ �÷��̾� ĳ������ ü���� �� �� ��ŭ �� �߰��� �����մϴ�."
            );

        public void ShowPermanentUpgradeIncreaceMoveSpeed() => Tip.Show(
            "Increase Move Speed Upgrade",
            "�������� Ŭ���� �������� ���� ������ �׸� �� '�̵� �ӵ� ����' ���׷��̵��� ȿ���� ���Դϴ�.\n\n�������� Ŭ���� �������� '�̵� �ӵ� ����' ���׷��̵带 ���� �� ������ �÷��̾� ĳ������ �̵� �ӵ��� �� �� % ��ŭ �� �߰��� �����մϴ�."
            );

        public void ShowPermanentUpgradeIncreaceCreditBonus() => Tip.Show(
            "Increase Move Speed Upgrade",
            "�������� Ŭ���� �������� ���� ������ �׸� �� 'ũ���� ȹ�淮 ����' ���׷��̵��� ȿ���� ���Դϴ�.\n\n�������� Ŭ���� �������� 'ũ���� ȹ�淮 ����' ���׷��̵带 ���� �� ������ ũ���� ȹ�淮 �������� �� �� % ��ŭ �� �߰��� �����մϴ�."
            );

        public void ShowPermanentUpgradeChoiceCount() => Tip.Show(
            "Increase Choice Count",
            "�������� Ŭ���� �������� ���� ������ �������� �þ�ϴ�.\n\n�⺻ ������ ������ ����� Stage Upgrade �޴����� ������ �� �ֽ��ϴ�."
            );

        //Stage Upgrade
        public void ShowStageUpgradeChoiceCount() => Tip.Show(
            "Increase Choice Count",
            "�������� Ŭ���� �������� ���� ������ ������ ������ �⺻ ���Դϴ�.\n\nPermanent Upgrade(�������� ���׷��̵�) �׸� �� ������ ������ ���̴� ���׷��̵尡 �����մϴ�."
            );

        public void ShowStageUpgradeHeal() => Tip.Show(
            "Heal",
            "ü���� ���� �� % ��ŭ ȸ���ϴ� �������Դϴ�."
            );

        public void ShowStageUpgradeCredit() => Tip.Show(
            "Credit",
            "���� ũ������ ȹ���մϴ�. ��� ���׷��̵尡 �����Ǿ� �� �̻� ������ �� �ִ� ���׷��̵尡 ���� ������ �����մϴ�."
            );

        public void ShowStageUpgradeWeight() => Tip.Show(
            "Weight",
            "���� ����ġ�Դϴ�. �� ������ ��� ���� ������ ���׷��̵��� ����ġ�� ���� ���� ���� ���� �� �������� ������ ������ Ȯ���� �˴ϴ�.\n\n�� ���׷��̵���� ���� �� ������ �ش� ���׷��̵��� ����ġ�� ������Ƿ� Ȯ���� ������ �߻��� �� �ֽ��ϴ�."
            );
    }
}