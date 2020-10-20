using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cheats : MonoBehaviour {
    #region NOLEER
    #region NOLEER2
    #region YOSEQUENOQUERESLEERLO
    [SerializeField] EnemyManager em;
    [SerializeField] BuildingCreator bc;
    [SerializeField] GameplayManager gm;
    [SerializeField] Town tn;
    #region enemies
    [SerializeField] Attacker attacker;
    [SerializeField] Tank tank;
    [SerializeField] BomberRat bomberrat;
    [SerializeField] Bard bard;

    [SerializeField] int damageAttacker;
    [SerializeField] int damageTank;
    [SerializeField] int damageBomberrat;

    [SerializeField] float attackSpeedAttacker;
    [SerializeField] float attackSpeedTank;

    [SerializeField] int hpAttacker;
    [SerializeField] int hpTank;
    [SerializeField] int hpBomberrat;
    [SerializeField] int hpBard;

    [SerializeField] int speedAttacker;
    [SerializeField] int speedTank;
    [SerializeField] int speedBomberrat;
    [SerializeField] int speedBard;
    [SerializeField] int healBard;
    #endregion

    #region structures
    [SerializeField] Cannon cannon;
    [SerializeField] Catapult catapult;
    [SerializeField] Crossbow crossbow;
    [SerializeField] Fence fence;
    [SerializeField] FlameThrower flamethrower;

    [SerializeField] int damageCannon;
    [SerializeField] int damageCatapult;
    [SerializeField] int damageCrossbow;
    [SerializeField] int damageFlameThrower;

    [SerializeField] float preparationTimeCannon;
    [SerializeField] float preparationTimeCatapult;
    [SerializeField] float preparationTimeCrossbow;
    [SerializeField] float preparationTimeFlameThrower;

    [SerializeField] int healthCannon;
    [SerializeField] int healthCatapult;
    [SerializeField] int healthCrossbow;
    [SerializeField] int healthFence;
    [SerializeField] int healthFlameThrower;

    [SerializeField] int toolsCostCannon;
    [SerializeField] int toolsCostCatapult;
    [SerializeField] int toolsCostCrossbow;
    [SerializeField] int toolsCostFence;
    [SerializeField] int toolsCostFlameThrower;

    [SerializeField] int initialToolsCostCannon;
    [SerializeField] int initialToolsCostCatapult;
    [SerializeField] int initialToolsCostCrossbow;
    [SerializeField] int initialToolsCostFence;
    [SerializeField] int initialToolsCostFlameThrower;

    #endregion

    bool cheatsActivated = false;
    public List<Enemy> enemies;
    public List<Building> builds;
    [SerializeField] GameObject UICheats;

    [SerializeField] TextMeshProUGUI[] textsStructures;
    [SerializeField] TextMeshProUGUI[] textsEnemies;
    private void Start() {
        textsEnemies[0].text = "Attacker DMG: "         + (damageAttacker = attacker.GetDamage()).ToString();
        textsEnemies[1].text = "Attacker AS: "          + (attackSpeedAttacker = attacker.GetTimeToAttack()).ToString();
        textsEnemies[2].text = "Attacker HP: "          + (hpAttacker = attacker.GetMaxHealth()).ToString();
        textsEnemies[3].text = "Attacker SPEED: "       + (speedAttacker = attacker.GetSpeed()).ToString();

        textsEnemies[4].text = "Tank DMG: "             + (damageTank = tank.GetDamage()                  ).ToString();     ;
        textsEnemies[5].text = "Tank AS: "              + (attackSpeedTank = tank.GetTimeToAttack()       ).ToString();     ;
        textsEnemies[6].text = "Tank HP: "              + (hpTank = tank.GetMaxHealth()                   ).ToString();     ;
        textsEnemies[7].text = "Tank SPEED: "           + (speedTank = tank.GetSpeed()                    ).ToString();     ;

        textsEnemies[8].text = "BomberRat DMG: "        +( damageBomberrat = bomberrat.GetDamage()   ).ToString()  ;
        textsEnemies[9].text = "BomberRat HP: "         +( hpBomberrat = bomberrat.GetMaxHealth()    ).ToString()  ;
        textsEnemies[10].text ="BomberRat SPEED: "      + ( speedBomberrat = bomberrat.GetSpeed()     ).ToString()  ;  

        textsEnemies[11].text = "Bard HP: "             + (hpBard = bard.GetMaxHealth()     ).ToString()   ;
        textsEnemies[12].text = "Bard SPEED: "          + (speedBard = bard.GetSpeed()      ).ToString()   ;
        textsEnemies[13].text = "Bard HEAL: "           + (healBard = bard.GetHeal()        ).ToString()   ;

        textsStructures[0].text = "Cannon COST: "   + (toolsCostCannon = cannon.GetToolsCost()              ) .ToString()  ;
        textsStructures[1].text = "Cannon AS: "     + (preparationTimeCannon = cannon.GetPreparationTime()  ) .ToString()  ;
        textsStructures[2].text = "Cannon HP: "     + (healthCannon = cannon.GetHP()                        ) .ToString()  ;
        textsStructures[3].text = "Cannon DMG: "    + (damageCannon = cannon.GetDamage()                    ) .ToString()  ;

        textsStructures[4].text ="Catapult COST: " +(toolsCostCatapult = catapult.GetToolsCost()                   ).ToString()  ;
        textsStructures[5].text ="Catapult AS: "   +(preparationTimeCatapult = catapult.GetPreparationTime()       ).ToString()  ;
        textsStructures[6].text ="Catapult HP: "   + (healthCatapult = catapult.GetHP()                             ).ToString()  ;
        textsStructures[7].text ="Catapult DMG: " +(damageCatapult = catapult.GetDamage()                         ).ToString()  ;

        textsStructures[8].text =  "Crossbow COST: "+(toolsCostCrossbow = crossbow.GetToolsCost()                 ).ToString()    ;
        textsStructures[9].text =  "Crossbow AS: " +       (preparationTimeCrossbow = crossbow.GetPreparationTime()     ).ToString()    ;
        textsStructures[10].text = "Crossbow HP: "    +    (healthCrossbow = crossbow.GetHP()                           ).ToString()    ;
        textsStructures[11].text = "Crossbow DMG: "    +   (damageCrossbow = crossbow.GetDamage()                       ).ToString()    ;

        textsStructures[12].text = "Fence COST: " + (toolsCostFence = fence.GetToolsCost()   ).ToString();
        textsStructures[13].text = "Fence HP: " + (healthFence = fence.GetHP()             ).ToString();

        textsStructures[14].text ="Flamethrower COST: "   +(toolsCostFlameThrower = flamethrower.GetToolsCost()               ).ToString()  ;
        textsStructures[15].text = "Flamethrower AS: " + (preparationTimeFlameThrower = flamethrower.GetPreparationTime()   ).ToString()  ;
        textsStructures[16].text = "Flamethrower HP: " + (healthFlameThrower = flamethrower.GetHP()                         ).ToString()  ;
        textsStructures[17].text = "Flamethrower DMG: " + (damageFlameThrower = flamethrower.GetDamage()                     ).ToString()  ;


        initialToolsCostCannon = toolsCostCannon;
        initialToolsCostCatapult = toolsCostCatapult;
        initialToolsCostCrossbow = toolsCostCrossbow;
        initialToolsCostFence = toolsCostFence;
        initialToolsCostFlameThrower = toolsCostFlameThrower;
    }
    private void OnDisable() {
        cannon.SetToolsCost(initialToolsCostCannon);
        catapult.SetToolsCost(initialToolsCostCatapult);
        crossbow.SetToolsCost(initialToolsCostCrossbow);
        fence.SetToolsCost(initialToolsCostFence);
        flamethrower.SetToolsCost(initialToolsCostFlameThrower);
    }

    private void OnDestroy() {
        cannon.SetToolsCost(initialToolsCostCannon);
        catapult.SetToolsCost(initialToolsCostCatapult);
        crossbow.SetToolsCost(initialToolsCostCrossbow);
        fence.SetToolsCost(initialToolsCostFence);
        flamethrower.SetToolsCost(initialToolsCostFlameThrower);
    }

    void Update() {
        if (!cheatsActivated) {
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                cheatsActivated = true;
                UICheats.SetActive(true);
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            cheatsActivated = false;
            UICheats.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) bc.AddTools(10000);

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            enemies = em.GetEnemies();
            int eCount = enemies.Count;
            for (int i = 0; i < eCount; i++) {
                enemies = em.GetEnemies();
                if (enemies[0] != null)
                    enemies[0].ReceiveDamage(99999);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) tn.ReceiveDamage(-99999);

        if (Input.GetKeyDown(KeyCode.Alpha5)) gm.StartHorde();

        if (Input.GetKeyDown(KeyCode.Alpha6)) gm.StopTimeCount();

        if (Input.GetKey(KeyCode.Alpha7)) {
            if (Input.GetKeyDown(KeyCode.Z)) damageAttacker++;
            else if (Input.GetKeyDown(KeyCode.X)) damageAttacker--;
            else if (Input.GetKeyDown(KeyCode.C)) attackSpeedAttacker += 0.1f;
            else if (Input.GetKeyDown(KeyCode.V)) { if (attackSpeedAttacker > 0.01f) attackSpeedAttacker -= 0.1f; }
            else if (Input.GetKeyDown(KeyCode.B)) {
                hpAttacker++;
                enemies = em.GetEnemies();
                for (int i = 0; i < enemies.Count; i++)
                    if (enemies[i] != null && enemies[i].type == Enemy.Type.Attacker)
                        enemies[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.N)) {
                hpAttacker--;
                for (int i = 0; i < enemies.Count; i++)
                    if (enemies[i] != null && enemies[i].type == Enemy.Type.Attacker)
                        enemies[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.M)) speedAttacker++;
            else if (Input.GetKeyDown(KeyCode.J)) speedAttacker--;
        }
        if (Input.GetKey(KeyCode.Alpha8)) {
            if (Input.GetKeyDown(KeyCode.Z)) damageTank++;
            else if (Input.GetKeyDown(KeyCode.X)) damageTank--;
            else if (Input.GetKeyDown(KeyCode.C)) attackSpeedTank += 0.1f;
            else if (Input.GetKeyDown(KeyCode.V)) { if (attackSpeedTank > 0.01f) attackSpeedTank -= 0.1f; }
            else if (Input.GetKeyDown(KeyCode.B)) {
                hpTank++;
                for (int i = 0; i < enemies.Count; i++)
                    if (enemies[i] != null && enemies[i].type == Enemy.Type.Tank)
                        enemies[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.N)) {
                hpTank--;
                for (int i = 0; i < enemies.Count; i++)
                    if (enemies[i] != null && enemies[i].type == Enemy.Type.Tank)
                        enemies[i].cheatsChangedHP = false;

            }
            else if (Input.GetKeyDown(KeyCode.M)) speedTank++;
            else if (Input.GetKeyDown(KeyCode.J)) speedTank--;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            if (Input.GetKeyDown(KeyCode.Z)) damageBomberrat++;
            else if (Input.GetKeyDown(KeyCode.X)) damageBomberrat--;
            else if (Input.GetKeyDown(KeyCode.B)) {
                hpBomberrat++;
                for (int i = 0; i < enemies.Count; i++)
                    if (enemies[i] != null && enemies[i].type == Enemy.Type.Bomberrat)
                        enemies[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.N)) {
                hpBomberrat--;
                for (int i = 0; i < enemies.Count; i++)
                    if (enemies[i] != null && enemies[i].type == Enemy.Type.Bomberrat)
                        enemies[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.M)) speedBomberrat++;
            else if (Input.GetKeyDown(KeyCode.J)) speedBomberrat--;
        }
        if (Input.GetKey(KeyCode.Alpha0)) {
            if (Input.GetKeyDown(KeyCode.B)) {
                hpBard++;
                for (int i = 0; i < enemies.Count; i++)
                    if (enemies[i] != null && enemies[i].type == Enemy.Type.Bard)
                        enemies[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.N)) {
                hpBard--;
                for (int i = 0; i < enemies.Count; i++)
                    if (enemies[i] != null && enemies[i].type == Enemy.Type.Bard)
                        enemies[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.M)) speedBard++;
            else if (Input.GetKeyDown(KeyCode.J)) speedBard--;
            else if (Input.GetKeyDown(KeyCode.K)) healBard++;
            else if (Input.GetKeyDown(KeyCode.L)) healBard--;
        }

        enemies = em.GetEnemies();
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i] != null) {
                switch (enemies[i].GetTypeOfEnemy()) {
                    case Enemy.Type.Attacker:
                        enemies[i].SetDamage(damageAttacker);
                        enemies[i].SetTimeToAttack(attackSpeedAttacker);
                        if (!enemies[i].cheatsChangedHP)
                            enemies[i].SetMaxHealth(hpAttacker);
                        enemies[i].SetSpeed(speedAttacker);
                        break;
                    case Enemy.Type.Tank:
                        enemies[i].SetDamage(damageTank);
                        enemies[i].SetTimeToAttack(attackSpeedTank);
                        if (!enemies[i].cheatsChangedHP)
                            enemies[i].SetMaxHealth(hpTank);
                        enemies[i].SetSpeed(speedTank);
                        break;
                    case Enemy.Type.Bomberrat:
                        enemies[i].SetDamage(damageBomberrat);
                        if (!enemies[i].cheatsChangedHP)
                            enemies[i].SetMaxHealth(hpBomberrat);
                        enemies[i].SetSpeed(speedBomberrat);
                        break;
                    case Enemy.Type.Bard:
                        if (!enemies[i].cheatsChangedHP)
                        enemies[i].SetMaxHealth(hpBard);
                            enemies[i].SetSpeed(speedBard);
                        enemies[i].GetComponent<Bard>().SetHeal(healBard);
                        break;
                    default:
                        Debug.LogError("Error en Cheats");
                        break;
                }
            }
        }

        builds = bc.GetBuilds();

        if (Input.GetKey(KeyCode.T)) {
            if (Input.GetKeyDown(KeyCode.Z)) damageCannon++;
            else if (Input.GetKeyDown(KeyCode.X)) damageCannon--;
            else if (Input.GetKeyDown(KeyCode.C)) preparationTimeCannon += 0.1f;
            else if (Input.GetKeyDown(KeyCode.V)) { if (preparationTimeCannon > 0.1f) preparationTimeCannon -= 0.1f; }
            else if (Input.GetKeyDown(KeyCode.B)) {
                healthCannon++;
                for (int i = 0; i < builds.Count; i++)
                    if (builds[i] != null && builds[i].type == Building.Type.Cannon)
                        builds[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.N)) {
                healthCannon--;
                for (int i = 0; i < builds.Count; i++)
                    if (builds[i] != null && builds[i].type == Building.Type.Cannon)
                        builds[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.M)) { toolsCostCannon += 25; cannon.SetToolsCost(toolsCostCannon);}
            else if (Input.GetKeyDown(KeyCode.J)) { toolsCostCannon -= 25; cannon.SetToolsCost(toolsCostCannon); }
        }
        if (Input.GetKey(KeyCode.Y)) {
            if (Input.GetKeyDown(KeyCode.Z)) damageCatapult++;
            else if (Input.GetKeyDown(KeyCode.X)) damageCatapult--;
            else if (Input.GetKeyDown(KeyCode.C)) preparationTimeCatapult += 0.1f;
            else if (Input.GetKeyDown(KeyCode.V)) { if (preparationTimeCatapult > 0.1f) preparationTimeCatapult -= 0.1f; }
            else if (Input.GetKeyDown(KeyCode.B)) {
                healthCatapult++;
                for (int i = 0; i < builds.Count; i++)
                    if (builds[i] != null && builds[i].type == Building.Type.Catapult)
                        builds[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.N)) {
                healthCatapult--;
                for (int i = 0; i < builds.Count; i++)
                    if (builds[i] != null && builds[i].type == Building.Type.Cannon)
                        builds[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.M)) { toolsCostCatapult += 25; catapult.SetToolsCost(toolsCostCatapult); }
            else if (Input.GetKeyDown(KeyCode.J)) {toolsCostCatapult -= 25; catapult.SetToolsCost(toolsCostCatapult); }
        }
        if (Input.GetKey(KeyCode.U)) {
            if (Input.GetKeyDown(KeyCode.Z)) damageCrossbow++;
            else if (Input.GetKeyDown(KeyCode.X)) damageCrossbow--;
            else if (Input.GetKeyDown(KeyCode.C)) preparationTimeCrossbow += 0.1f;
            else if (Input.GetKeyDown(KeyCode.V)) { if (preparationTimeCrossbow > 0.1f) preparationTimeCrossbow -= 0.1f; }
            else if (Input.GetKeyDown(KeyCode.B)) {
                healthCrossbow++;
                for (int i = 0; i < builds.Count; i++)
                    if (builds[i] != null && builds[i].type == Building.Type.Crossbow)
                        builds[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.N)) {
                healthCrossbow--;
                for (int i = 0; i < builds.Count; i++)
                    if (builds[i] != null && builds[i].type == Building.Type.Crossbow)
                        builds[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.M)) { toolsCostCrossbow += 25; crossbow.SetToolsCost(toolsCostCrossbow); }
            else if (Input.GetKeyDown(KeyCode.J)) { toolsCostCrossbow -= 25; crossbow.SetToolsCost(toolsCostCrossbow);}
        }
        if (Input.GetKey(KeyCode.I)) {
            if (Input.GetKeyDown(KeyCode.B)) {
                healthFence++;
                for (int i = 0; i < builds.Count; i++)
                    if (builds[i] != null && builds[i].type == Building.Type.Fence)
                        builds[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.N)) {
                healthFence--;
                for (int i = 0; i < builds.Count; i++)
                    if (builds[i] != null && builds[i].type == Building.Type.Fence)
                        builds[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.M)) { toolsCostFence += 25; fence.SetToolsCost(toolsCostFence);}
            else if (Input.GetKeyDown(KeyCode.J)) { toolsCostFence -= 25; fence.SetToolsCost(toolsCostFence); }
        }
        if (Input.GetKey(KeyCode.O)) {
            if (Input.GetKeyDown(KeyCode.Z)) damageFlameThrower++;
            else if (Input.GetKeyDown(KeyCode.X)) damageFlameThrower--;
            else if (Input.GetKeyDown(KeyCode.C)) preparationTimeFlameThrower += 0.1f;
            else if (Input.GetKeyDown(KeyCode.V)) { if (preparationTimeFlameThrower > 0.1f) preparationTimeFlameThrower -= 0.1f; }
            else if (Input.GetKeyDown(KeyCode.B)) {
                healthFlameThrower++;
                for (int i = 0; i < builds.Count; i++)
                    if (builds[i] != null && builds[i].type == Building.Type.Flamethrower)
                        builds[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.N)) {
                healthFlameThrower--;
                for (int i = 0; i < builds.Count; i++)
                    if (builds[i] != null && builds[i].type == Building.Type.Flamethrower)
                        builds[i].cheatsChangedHP = false;
            }
            else if (Input.GetKeyDown(KeyCode.M)) { toolsCostFlameThrower += 25; flamethrower.SetToolsCost(toolsCostCrossbow); }
            else if (Input.GetKeyDown(KeyCode.J)) { toolsCostFlameThrower -= 25; flamethrower.SetToolsCost(toolsCostCrossbow); }
        }

        for (int i = 0; i < builds.Count; i++) {
            if (builds[i] != null)
                switch (builds[i].GetTypeOfBuild()) {
                    case Building.Type.Cannon:
                        builds[i].SetDamage(damageCannon);
                        if (!builds[i].cheatsChangedHP)
                            builds[i].SetHP(healthCannon);
                        builds[i].SetPreparationTime(preparationTimeCannon);
                        builds[i].SetToolsCost(toolsCostCannon);
                        break;
                    case Building.Type.Catapult:
                        builds[i].SetDamage(damageCatapult);
                        if (!builds[i].cheatsChangedHP)
                            builds[i].SetHP(healthCatapult);
                        builds[i].SetPreparationTime(preparationTimeCatapult);
                        builds[i].SetToolsCost(toolsCostCatapult);
                        break;
                    case Building.Type.Crossbow:
                        builds[i].SetDamage(damageCrossbow);
                        if (!builds[i].cheatsChangedHP)
                            builds[i].SetHP(healthCrossbow);
                        builds[i].SetPreparationTime(preparationTimeCrossbow);
                        builds[i].SetToolsCost(toolsCostCrossbow);
                        break;
                    case Building.Type.Fence:
                        if (!builds[i].cheatsChangedHP)
                            builds[i].SetHP(healthFence);
                        builds[i].SetToolsCost(toolsCostFence);
                        break;
                    case Building.Type.Flamethrower:
                        builds[i].SetDamage(damageFlameThrower);
                        if (!builds[i].cheatsChangedHP)
                            builds[i].SetHP(healthFlameThrower);
                        builds[i].SetPreparationTime(preparationTimeFlameThrower);
                        builds[i].SetToolsCost(toolsCostFlameThrower);
                        break;
                    default:
                        Debug.LogError("ERror Cheats Builds");
                        break;
                }
        }
    }

    private void FixedUpdate() {
        textsEnemies[0].text = "Attacker DMG: " + (damageAttacker);
        textsEnemies[1].text = "Attacker AS: " + (attackSpeedAttacker);
        textsEnemies[2].text = "Attacker HP: " + (hpAttacker);
        textsEnemies[3].text = "Attacker SPEED: " + (speedAttacker);

        textsEnemies[4].text = "Tank DMG: " + (damageTank);
        textsEnemies[5].text = "Tank AS: " + (attackSpeedTank);
        textsEnemies[6].text = "Tank HP: " + (hpTank);
        textsEnemies[7].text = "Tank SPEED: " + (speedTank);

        textsEnemies[8].text = "BomberRat DMG: " + (damageBomberrat);
        textsEnemies[9].text = "BomberRat HP: " + (hpBomberrat);
        textsEnemies[10].text = "BomberRat SPEED: " + (speedBomberrat);

        textsEnemies[11].text = "Bard HP: " + (hpBard);
        textsEnemies[12].text = "Bard SPEED: " + (speedBard);
        textsEnemies[13].text = "Bard HEAL: " + (healBard);

        textsStructures[0].text = "Cannon COST: " + (toolsCostCannon);
        textsStructures[1].text = "Cannon AS: " + (preparationTimeCannon);
        textsStructures[2].text = "Cannon HP: " + (healthCannon);
        textsStructures[3].text = "Cannon DMG: " + (damageCannon);

        textsStructures[4].text = "Catapult COST: " + (toolsCostCatapult);
        textsStructures[5].text = "Catapult AS: " + (preparationTimeCatapult);
        textsStructures[6].text = "Catapult HP: " + (healthCatapult);
        textsStructures[7].text = "Catapult DMG: " + (damageCatapult);

        textsStructures[8].text = "Crossbow COST: " + (toolsCostCrossbow);
        textsStructures[9].text = "Crossbow AS: " + (preparationTimeCrossbow);
        textsStructures[10].text = "Crossbow HP: " + (healthCrossbow);
        textsStructures[11].text = "Crossbow DMG: " + (damageCrossbow);

        textsStructures[12].text = "Fence COST: " + (toolsCostFence);
        textsStructures[13].text = "Fence HP: " + (healthFence);

        textsStructures[14].text = "Flamethrower COST: " + (toolsCostFlameThrower);
        textsStructures[15].text = "Flamethrower AS: " + (preparationTimeFlameThrower);
        textsStructures[16].text = "Flamethrower HP: " + (healthFlameThrower);
        textsStructures[17].text = "Flamethrower DMG: " + (damageFlameThrower);
    }
    #endregion
    #endregion
    #endregion
}