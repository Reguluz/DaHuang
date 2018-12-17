using System;
using System.Collections;
using CenterSystem;
using Item.AttackProps;
using UnityEngine;
using Weapons.Gun;
using Random = UnityEngine.Random;



namespace Item
{
    public class ItemFactory : MonoBehaviour
    {
        public ItemType Createtype;
        public float Intervaltime;
        public int MaxNumber;
        
        private Sprite _area;
        private Transform _transform;
        private Vector3 _minPosition;
        private Vector3 _maxPosition;
        
        public GameObject ItemContainer;    //获取该区域对应的生成物体集合的父物体
    

        void Awake()
        {
            
            _area = this.gameObject.GetComponent<SpriteRenderer>().sprite;        //获取区域贴图
            _transform = this.gameObject.GetComponent<Transform>();                
            if (_area.texture != null)                                            //当区域贴图不为空时获得贴图的最小坐标（x最小与y最小）和最大坐标（x最大与y最大）来获取生成区域
            {
                _minPosition = new Vector3((_transform.position.x - (_area.texture.width >> 7) * gameObject.transform.localScale.x),
                    (_transform.position.y - (_area.texture.height >> 7) * gameObject.transform.localScale.y),
                    SystemOption.ItemZPosition);
                _maxPosition = new Vector3((_transform.position.x + (_area.texture.width >> 7) * gameObject.transform.localScale.x),
                    (_transform.position.y + (_area.texture.height >> 7) * gameObject.transform.localScale.y),
                    SystemOption.ItemZPosition);
            }
            
            CheckCreateType();        //检索生成类别
        }

        private void Update()        //Debug显示区域对角线
        {
            Debug.DrawLine(_minPosition,_maxPosition,Color.cyan);
        }

        private void CheckCreateType()
        {
            switch (Createtype)        //对应不同生成类别执行不同协程
            {
                    case ItemType.Gun: 
                        StartCoroutine(CreateGun());
                        break;
                    case ItemType.Medecine : 
                        StartCoroutine(CreateMedecine());
                        break;
                    case ItemType.AttackProps : 
                        StartCoroutine(CreateAttackProp());
                        break;
                    default: break;
            }
        }

        IEnumerator CreateGun()
        {
            while (true)
            {
                if (ItemContainer.transform.childCount < MaxNumber)        //统计ItemContainer的子物体数量小于设置最大值时
                {
                    var created = PublicFunction.RandomEnum<Gunname>();    //调用单例PublicFunction中随机选择枚举的函数并返回一个随机枚举值
                    GameObject newGun = Instantiate((GameObject) Resources.Load("Gun", typeof(GameObject)),new Vector3(    //生成这个Gun Prefab，坐标为区域内的随机值（Z轴坐标固定）
                            Random.Range(_minPosition.x,_maxPosition.x),
                            Random.Range(_minPosition.y,_maxPosition.y),
                            SystemOption.ItemZPosition),
                        Quaternion.identity);
                    newGun.transform.parent = ItemContainer.transform;    //将生成的物体设置为ItemContainer的子物体
                    newGun.transform.localScale = new Vector3(SystemOption.NormalGunScale,SystemOption.NormalGunScale,SystemOption.NormalGunScale);    //设置物体比例
                    GunAction action = newGun.GetComponent<GunAction>();    //获取生成物体的GunAction
                    action.Chosen = created;                                //设置GunAction的Chosen枚举为上文随机生成的枚举值
                    action.Init();                                           //初始化GunAction的设置
                    //action.Gun.SetBullet(-1);
                }
                yield return new WaitForSeconds(Intervaltime);
            }
            
        }

        IEnumerator CreateAttackProp()
        {
            while (true)
            {
                if (ItemContainer.transform.childCount < MaxNumber)
                {
                    var created = PublicFunction.RandomEnum<AttackProps.AttackProps>();
                    GameObject newProps = Instantiate((GameObject) Resources.Load("Props", typeof(GameObject)),
                        new Vector3(
                            Random.Range(_minPosition.x, _maxPosition.x),
                            Random.Range(_minPosition.y, _maxPosition.y),
                            SystemOption.ItemZPosition),
                        Quaternion.identity);
                    AttackPropsAction action = newProps.GetComponent<AttackPropsAction>();
                    action.Chosen = created;
                    action.Init();
                    newProps.transform.parent = ItemContainer.transform;
                    newProps.transform.localScale = new Vector3(SystemOption.SceneScale,
                    SystemOption.SceneScale, SystemOption.SceneScale);
                }

                yield return new WaitForSeconds(Intervaltime);
            }
            
        }

        IEnumerator CreateMedecine()
        {
            while (true)
            {
                if (ItemContainer.transform.childCount < MaxNumber)
                {
                    GameObject newRecoverBag = Instantiate(
                        (GameObject) Resources.Load("RecoverBag", typeof(GameObject)), new Vector3(
                            Random.Range(_minPosition.x, _maxPosition.x),
                            Random.Range(_minPosition.y, _maxPosition.y),
                            SystemOption.ItemZPosition),
                        Quaternion.identity);
                    newRecoverBag.transform.parent = ItemContainer.transform;
                    newRecoverBag.transform.localScale = new Vector3(1,1,1);
                }

                yield return new WaitForSeconds(Intervaltime);
            }
            
        }
    }
}
