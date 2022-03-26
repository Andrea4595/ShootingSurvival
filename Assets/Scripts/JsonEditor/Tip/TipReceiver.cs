using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class TipReceiver : MonoBehaviour
    {
        //Todo ���

        public void ShowAutoSave() => Tip.Show(
            "�ڵ� ����",
            "�������� �߻� �� �ڵ����� ���������� �����մϴ�. �ڵ� ���� �ÿ��� �Ź� ��� ������ �����˴ϴ�."
            );

        //Character
        public void ShowCharacterWeapons() => Tip.Show(
            "���� ����",
            "�� ĳ���Ͱ� ������ �����Դϴ�. �� ĳ���Ͱ� �������� ���⸦ ���� �� �ֽ��ϴ�.\n\nplayer ĳ���Ϳ��� ���Ⱑ �ο��� ��� ������ �� �⺻������ �����ϴ� ���Ⱑ �˴ϴ�."
            );

        public void ShowCharacterHomming() => Tip.Show(
            "��ȸ��",
            "��ǥ�� ���� ��ȸ�ϴ� �ӵ��Դϴ�.\n\n�÷��̾� ĳ���ʹ� Ŀ����, �� ĳ���͵��� �÷��̾� ĳ���͸� ���� ��ȸ�մϴ�."
            );

        //Weapon
        public void ShowWeaponForPlayer() => Tip.Show(
            "�÷��̾� ����",
            "�� ���� üũ �� ���, ���� �� ��ȭ ���������� �� ������ ȹ�� �Ǵ� ��ȭ�� ������ �� �ְ� �˴ϴ�."
            );

        public void ShowWeaponProjectileHp() => Tip.Show(
            "ü��",
            "ü���� �����ϴ� ����ü�� ���� ������ �޾� �ı��� �� �ֽ��ϴ�.\n\n0���� ���� �� �ı����� �ʴ� ����ü�� �˴ϴ�."
            );

        public void ShowWeaponProjectileExplosionRange() => Tip.Show(
            "���� ����",
            "Ÿ�� �������� �� ������ ���� �� ��ŭ�� ������ �߻��մϴ�. ���� ȿ���� 1�ʰ� �����Ǹ�, �� ���� ���� �� ĳ���� Ȥ�� �ı� ������ �� ����ü�� �����մϴ�.\n\n���� ü���� �����Ǿ� �ִٸ�, ���ݹ޾� ����ü�� ���ŵ� �� �����մϴ�.\n\n0���� ���� �� �������� �ʰ� ���� ���� ��󿡰Ը� ���ظ� �����ϴ�."
            );

        public void ShowWeaponProjectileHomming() => Tip.Show(
            "��ȸ��",
            "���� ����� �� ĳ���͸� ���� ��ȸ�մϴ�. �� ���� ���� ���� �� ũ�� ��ȸ�մϴ�."
            );

        public void ShowWeaponProjectileLifetime() => Tip.Show(
            "���� �ð�",
            "�߻�� ���� ���� �� �� ��ŭ�� �� ���� Ȱ��ȭ �˴ϴ�. �ð��� ������ ����ü�� �������, ���� ������ ���� �� ����ü�� ��� �� �ڸ����� �����մϴ�."
            );

        public void ShowWeaponProjectileHitProjectile() => Tip.Show(
            "����ü Ÿ�� ����",
            "�� ���� üũ �� ��� Hp�� ������ �� ����ü�� Ÿ���� �� �ְ� �˴ϴ�."
            );

        public void ShowWeaponFireType() => Tip.Show(
            "��� ���",
            "�� ���Ⱑ ����ü�� � ������� �߻����� �����մϴ�.\n\nFrontEvenSpread:\n������ ���� ��簢 �� �յ��� ������ ����ü���� �л���� �߻��մϴ�.\n\nFrontArrow:\n������ ���� ȭ���� ���·� ����ü���� ���Ľ��� �߻��մϴ�. ��簢�� ������ �޽��ϴ�.\n\nRandom:\n��簢 �� �������� �������� ����ü�� �߻��մϴ�."
            );

        public void ShowWeaponFireCount() => Tip.Show(
            "���� �߻� ����",
            "�� ���� �߻�Ǵ� ����ü�� �����Դϴ�."
            );

        public void ShowWeaponContinuousCount() => Tip.Show(
            "���� Ƚ��",
            "�� ���� �����ϴ� Ƚ���Դϴ�.\n\n�� ����� ���������� 0.1���� ������ �����ϴ�."
            );

        public void ShowWeaponInterval() => Tip.Show(
            "���� ��� �ð�",
            "���� �Է� �� �ʸ��� �߻��մϴ�."
            );

        public void ShowWeaponUpgrade() => Tip.Show(
            "��ȭ",
            "���� �� ��ȭ �ܰ迡�� � ��ȭ�� �ִ����� �����մϴ�.\n�Է��� ������ ������ ������� ���۵˴ϴ�."
            );

        //Permanent Upgrade
        public void ShowPermanentUpgradeIncreaceHp() => Tip.Show(
            "ü�� ����",
            "player ĳ������ �⺻ ü�¿� �������ϴ�."
            );

        public void ShowPermanentUpgradeIncreaceDamage() => Tip.Show(
            "���ݷ� ����",
            "player ĳ���Ͱ� �߻��ϴ� ��� ����ü�� ���ݷ��� �����մϴ�."
            );

        public void ShowPermanentUpgradeIncreaceMoveSpeed() => Tip.Show(
            "�̵� �ӵ� ����",
            "player ĳ������ �⺻ �̵��ӵ��� �������ϴ�."
            );

        public void ShowPermanentUpgradeIncreaceCreditBonus() => Tip.Show(
            "ũ���� ���ʽ� ����",
            "�⺻ ũ���� ȹ�淮�� �������ϴ�."
            );

        public void ShowPermanentUpgradeChoiceCount() => Tip.Show(
            "��ȭ ������ ����",
            "�������� Ŭ���� �������� ���� ������ �������� �þ�ϴ�.\n\n�⺻ ������ ������ ����� '���� �� ��ȭ' �޴����� ������ �� �ֽ��ϴ�."
            );

        //Stage Upgrade
        public void ShowStageUpgradeChoiceCount() => Tip.Show(
            "������ ����",
            "�������� Ŭ���� �������� ���� ������ ������ ������ �⺻ ���Դϴ�.\n\n������ ��ȭ �׸� �� ������ ������ ���̴� ��ȭ�� �����մϴ�."
            );

        public void ShowStageUpgradeHeal() => Tip.Show(
            "ȸ��",
            "ü���� ���� �� �� ��ŭ ȸ���ϴ� �������Դϴ�.\n\n���ظ� ���� ��Ȳ������ �����մϴ�."
            );

        public void ShowStageUpgradeCredit() => Tip.Show(
            "ũ����",
            "���� ũ������ ȹ���մϴ�. ��� ��ȭ�� �����Ǿ� �� �̻� ������ �� �ִ� ��ȭ�� ���� ������ �����մϴ�.\n\n�� �������� �����Ǵ� ũ������ ũ���� ȹ�淮 ���� �ɼ��� �ǽð����� ���� �޽��ϴ�."
            );

        public void ShowStageUpgradeWeight() => Tip.Show(
            "����ġ",
            "���� ����ġ�Դϴ�. �� ������ ��� ���� ������ ��ȭ�� ����ġ�� ���� ���� ���� ���� �� �������� ������ ������ Ȯ���� �˴ϴ�.\n\n�� ��ȭ���� ���� �� ������ �ش� ��ȭ�� ����ġ�� ������Ƿ� Ȯ���� ������ �߻��� �� �ֽ��ϴ�."
            );
    }
}