using System;
using System.Collections.Generic;
using UnityEngine;

// 드롭 컴포넌트 픽업 시 발생 가능한 단일 결과(카드 표시 + 적용 동작).
public class PickupOutcome
{
    public UpgradeOption Display;
    public Action Apply;
}

// 드롭된 장비 정의 + 현재 슬롯 상태로 픽업 결과 목록을 만드는 파서.
// index 0 = 비-교체 기본 결과(빈 슬롯=장착 / 강화 / 최대레벨=모디파이어).
// Named는 [0]만 자동 적용, 플레이어는 전체를 카드로 표시한다.
public static class PickupOutcomes
{
    public static List<PickupOutcome> Build(
        UpgradeDefinition equipment, ShipComponent ship, ShipStats stats, UpgradePool pool)
    {
        var list = new List<PickupOutcome>();

        var attachable = GetAttachable(equipment);
        if (attachable == null) return list;

        var baseDisplay = equipment.BuildDisplay(ship, stats);

        // 빈 슬롯: 드롭품 장착 (선택지 없음)
        if (ship.IsEmpty(attachable))
        {
            list.Add(new PickupOutcome
            {
                Display = new UpgradeOption
                {
                    Icon = baseDisplay.Icon,
                    Name = baseDisplay.Name,
                    Level = 1,
                    Description = baseDisplay.Description,
                },
                Apply = () => ship.Install(attachable),
            });
            return list;
        }

        // index 0: 강화 가능하면 현재 슬롯 레벨업, 최대 레벨이면 랜덤 모디파이어.
        if (ship.SlotCanUpgrade(attachable))
        {
            var equipped = GetEquipped(ship, attachable);
            var equippedDef = equipped != null ? FindDefinitionByType(pool, equipped.GetType()) : null;

            var display = equippedDef != null
                ? equippedDef.BuildDisplay(ship, stats)
                : new UpgradeOption
                {
                    Icon = baseDisplay.Icon,
                    Name = baseDisplay.Name,
                    Level = ship.GetLevel(attachable) + 1,
                    Description = baseDisplay.Description,
                };

            list.Add(new PickupOutcome
            {
                Display = display,
                Apply = () => ship.UpgradeCurrentSlot(attachable),
            });
        }
        else
        {
            var mod = PickRandomModifier(pool);
            if (mod != null)
            {
                list.Add(new PickupOutcome
                {
                    Display = mod.BuildDisplay(ship, stats),
                    Apply = () => mod.Apply(ship, stats),
                });
            }
        }

        // 다른 타입일 때 드롭품 Lv1로 교체 선택지
        if (!ship.IsSameTypeEquipped(attachable))
        {
            list.Add(new PickupOutcome
            {
                Display = new UpgradeOption
                {
                    Icon = baseDisplay.Icon,
                    Name = baseDisplay.Name,
                    Level = 1,
                    Description = baseDisplay.Description,
                },
                Apply = () => ship.Replace(attachable),
            });
        }

        return list;
    }

    private static StatModifierUpgrade PickRandomModifier(UpgradePool pool)
    {
        var mods = pool != null ? pool.ModifierDefinitions : null;
        if (mods == null || mods.Count == 0) return null;
        return mods[UnityEngine.Random.Range(0, mods.Count)];
    }

    private static IAttachable GetAttachable(UpgradeDefinition def) => def switch
    {
        MainEquipment m => m.Attachable,
        SubEquipment s => s.Attachable,
        RearEquipment r => r.Attachable,
        _ => null
    };

    // 드롭품 타입으로 슬롯을 지목해 현재 장착된 인스턴스를 반환.
    private static IAttachable GetEquipped(ShipComponent ship, IAttachable dropped) => dropped switch
    {
        MainAttachableBase _ => ship.MainSlot,
        SubAttachableBase _ => ship.SubSlot,
        RearAttachableBase _ => ship.RearSlot,
        _ => null
    };

    // 풀에서 주어진 attachable 타입과 동일한 장비 정의를 찾는다(표시용 역참조).
    private static UpgradeDefinition FindDefinitionByType(UpgradePool pool, Type attachableType)
    {
        if (pool == null) return null;
        foreach (var d in pool.AttachmentDefinitions)
        {
            var a = GetAttachable(d);
            if (a != null && a.GetType() == attachableType) return d;
        }
        return null;
    }
}
