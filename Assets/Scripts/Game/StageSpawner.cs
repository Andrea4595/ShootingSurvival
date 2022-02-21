using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class StageSpawner : Singleton<StageSpawner>
    {
        List<Character> _remains = new List<Character>();

        public Character[] Remains(Character.Force ownerForce)
        {
            if (ownerForce == Character.Force.Player)
                return _remains.ToArray();
            else
                return new Character[1] { PlayerSetter.instance.player };
        }

        private void Awake()
        {
            Initialize(this);
        }

        private void OnEnable()
        {
            StartCoroutine(CRun());
        }

        IEnumerator CRun()
        {
            foreach (var stage in Data.GameData.instance.stages)
                yield return CStage(stage);
        }

        IEnumerator CStage(Data.Object.Stage stage)
        {
            foreach (var group in stage.groups)
            {
                SpawnCharacterGrop(group.spawns);

                while (_remains.Count > 0)
                    yield return null;
            }

            OfferReward(stage.credit);
        }

        void SpawnCharacterGrop(Data.Object.Stage.Spawn[] spawns)
        {
            foreach (var spawn in spawns)
            {
                var key = Data.GameData.instance.GetCharacterData(spawn.key);

                for (int i = 0; i < spawn.count; i++)
                {
                    var character = ObjectPool<Character>.GetObject();
                    character.Initialize(key, Character.Force.Enemy);
                    SetPosition(character);
                    AddMovementToCharacter(character);
                    _remains.Add(character);
                    character.onDestroy += () => { _remains.Remove(character); };
                }
            }
        }

        void SetPosition(Character character)
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

        void AddMovementToCharacter(Character character)
        {
            character.gameObject.AddComponent<ChasingPlayer>().Initialize(character);
        }

        void OfferReward(int credit)
        {

        }
    }
}