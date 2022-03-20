using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class StageSpawner : Singleton<StageSpawner>
    {
        [SerializeField]
        UI.GameOver _gameOver;
        [SerializeField]
        UI.GameOver _clear;
        [SerializeField]
        bool _stageStart = true;

        List<Character.Character> _remains = new List<Character.Character>();
        
        public int creditReward;

        public Character.Character[] Remains(Character.Character.Force targetForce)
        {
            if (targetForce == Character.Character.Force.Player)
                return new Character.Character[1] { PlayerSetter.instance.player };
            else
                return _remains.ToArray();
        }

        public Character.Character[] TargetRemains(Character.Character.Force ownerForce)
        {
            if (ownerForce == Character.Character.Force.Player)
                return Remains(Character.Character.Force.Enemy);
            else
                return Remains(Character.Character.Force.Player);
        }

        public void GameOver()
        {
            _gameOver?.Run(creditReward);
        }

        public void Clear()
        {
            _clear?.Run(creditReward);
        }

        public void AddRemain(Character.Character character)
        {
            _remains.Add(character);
        }

        private void Awake()
        {
            Data.GameData.instance.GameInitialize();
            Time.instance.Fade(1, 0);
        }

        private void OnEnable()
        {
            if (!_stageStart)
                return;

            StartCoroutine(CRun());
        }

        IEnumerator CRun()
        {
            var stages = Data.GameData.instance.stages;
            for (var i = 0; i < stages.Length; i++)
            {
                yield return CStage(stages[i]);

                OfferReward(stages[i].credit);

                if (i >= stages.Length - 1)
                    break;

                ShowUpgrade();
            }

            Clear();
        }

        IEnumerator CStage(Data.Object.StageInformation stage)
        {
            foreach (var group in stage.groups)
            {
                SpawnCharacterGrop(group.spawns);

                while (_remains.Count > 0)
                    yield return null;
            }
        }

        void SpawnCharacterGrop(Data.Object.StageInformation.Group.Spawn[] spawns)
        {
            foreach (var spawn in spawns)
            {
                var key = Data.GameData.instance.GetCharacterInformation(spawn.key);

                for (int i = 0; i < spawn.count; i++)
                {
                    var character = ObjectPool<Character.Character>.instance.GetObject();
                    character.Initialize(key, Character.Character.Force.Enemy);
                    SetPosition(character);
                    SetRotation(character);
                    AddMovementToCharacter(character);
                    _remains.Add(character);
                    character.onDestroy += () => { _remains.Remove(character); };
                }
            }
        }

        void SetPosition(Character.Character character)
        {
            Rect SpawnBorder()
            {
                var camera = Camera.main;
                var width = camera.orthographicSize * Screen.width / Screen.height;
                var height = camera.orthographicSize;

                return new Rect(camera.transform.position.x - width, camera.transform.position.y - height, width * 2, height * 2);
            }

            var newPosition = new Vector3();
            var spawnBorder = SpawnBorder();

            if (Random.Range(0, 2) == 0)
            {
                newPosition.x = Random.Range(spawnBorder.xMin, spawnBorder.xMax);
                newPosition.y = Random.Range(0, 2) == 0 ? spawnBorder.yMin : spawnBorder.yMax;
            }
            else
            {
                newPosition.x = Random.Range(0, 2) == 0 ? spawnBorder.xMin : spawnBorder.xMax;
                newPosition.y = Random.Range(spawnBorder.yMin, spawnBorder.yMax);
            }

            character.movement.SetPosition(newPosition);
        }

        void SetRotation(Character.Character character)
        {
            var player = PlayerSetter.instance.player;
            var vector = player.movement.position - character.movement.position;
            var direction = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

            character.movement.LookAtDirection(direction);
        }

        void AddMovementToCharacter(Character.Character character)
        {
            character.gameObject.AddComponent<Character.ChasingPlayer>().Initialize(character);
        }

        void OfferReward(int credit)
        {
            creditReward += Mathf.RoundToInt(credit * Data.GameData.instance.creditBonus);
        }

        void ShowUpgrade()
        {
            UI.StageUpgradeSelector.instance.Show();
        }
    }
}