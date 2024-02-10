using Fitness.Core.Entities;
using Fitness.Dtos;

namespace Fitness.Helpers;
public class TreeBuilder
{
    public static List<TreeNodeDto> ModuleTree(List<Module> data)
    {
        List<TreeNodeDto> treeNodeDtos = new List<TreeNodeDto>();
        var rootModules = data.Where(item => item.ParentModuleId == null).ToList();
        foreach (var rootModule in rootModules)
        {
            TreeNodeDto rootNode = new TreeNodeDto();
            rootNode.Key = rootModule.Id.ToString();
            rootNode.Label = rootModule.Name;
            rootNode.Children = OperationToNodes(rootModule.ModuleOperations.ToList());

            var parentModules = data.Where(item => item.ParentModuleId == rootModule.Id).ToList();
            foreach (var parentModule in parentModules)
            {
                TreeNodeDto parentNode = new TreeNodeDto();
                parentNode.Key = parentModule.Id.ToString();//rootModule.Id.ToString() + "-" + parentModule.Id.ToString();
                parentNode.Label = parentModule.Name;
                parentNode.Children = OperationToNodes(parentModule.ModuleOperations.ToList());

                var childModules = data.Where(item => item.ParentModuleId == parentModule.Id).ToList();
                foreach (var childModule in childModules)
                {
                    TreeNodeDto childNode = new TreeNodeDto();
                    childNode.Key = childModule.Id.ToString();// rootModule.Id.ToString() + "-" + parentModule.Id.ToString() + "-" + childModule.Id.ToString();
                    childNode.Label = childModule.Name;
                    childNode.Children = OperationToNodes(childModule.ModuleOperations.ToList());

                    var bottomModules = data.Where(item => item.ParentModuleId == childModule.Id).ToList();
                    foreach (var bottomModule in bottomModules)
                    {
                        TreeNodeDto bottomNode = new TreeNodeDto();
                        bottomNode.Key = bottomModule.Id.ToString();//rootModule.Id.ToString() + "-" + parentModule.Id.ToString() + "-" + childModule.Id.ToString() + "-" + bottomModule.Id.ToString();
                        bottomNode.Label = bottomModule.Name;
                        bottomNode.Children = OperationToNodes(bottomModule.ModuleOperations.ToList());
                        childNode.Children.Add(bottomNode);
                    }
                    parentNode.Children.Add(childNode);
                }
                rootNode.Children.Add(parentNode);
            }
            treeNodeDtos.Add(rootNode);
        }
        return treeNodeDtos;
    }

    public static List<AuthModuleDto> AuthModuleTree(List<Module> data)
    {
        List<AuthModuleDto> treeNodeDtos = new List<AuthModuleDto>();
        var rootModules = data.Where(item => item.ParentModuleId == null).ToList();
        foreach (var rootModule in rootModules)
        {
            AuthModuleDto rootNode = ModuleToAuthModule(rootModule);
            var parentModules = data.Where(item => item.ParentModuleId == rootModule.Id).ToList();
            foreach (var parentModule in parentModules)
            {
                AuthModuleDto parentNode = ModuleToAuthModule(parentModule);
                var childModules = data.Where(item => item.ParentModuleId == parentModule.Id).ToList();
                foreach (var childModule in childModules)
                {
                    AuthModuleDto childNode = ModuleToAuthModule(childModule);
                    var bottomModules = data.Where(item => item.ParentModuleId == childModule.Id).ToList();
                    foreach (var bottomModule in bottomModules)
                    {
                        AuthModuleDto bottomNode = ModuleToAuthModule(bottomModule);
                        childNode.Items.Add(bottomNode);
                    }
                    parentNode.Items.Add(childNode);
                }
                rootNode.Items.Add(parentNode);
            }
            treeNodeDtos.Add(rootNode);
        }
        return treeNodeDtos;
    }

    public static Dictionary<string, RoleModuleNodeDto> BuildRoleModuleTree(List<RoleModuleDetailsDto> data)
    {
        var moduleNodeDtos = new Dictionary<string, RoleModuleNodeDto>();
        data.ForEach(roleModule =>
        {
            var split = roleModule.Module.Hierarchy.Split('-');
            roleModule.RoleModuleOperations.ToList().ForEach(op =>
            {
                var key = $"{(split.Length > 1 ? split[^1] : roleModule.Module.Hierarchy)}-{op.OperationId}";
                if (!moduleNodeDtos.ContainsKey(key))
                {
                    moduleNodeDtos.Add($"{(split.Length > 1 ? split[^1] : roleModule.Module.Hierarchy)}-{op.OperationId}", new RoleModuleNodeDto { Checked = true });
                }
            });
        });

        return moduleNodeDtos;
    }

    private static AuthModuleDto ModuleToAuthModule(Module module)
    {
        AuthModuleDto item = new AuthModuleDto();
        item.Id = module.Id;
        item.Label = module.Name;
        item.Url = module.Url;
        item.Icon = module.Icon;
        item.Operations = module.ModuleOperations.Select(r => r.Operation).Select(o => new AuthModuleDto { Id = o.Id, Label = o.Name }).ToList();
        item.Items = new List<AuthModuleDto>();
        return item;
    }

    private static List<TreeNodeDto> OperationToNodes(List<ModuleOperation> moduleOperations)
    {
        List<TreeNodeDto> items = new List<TreeNodeDto>();
        foreach (var moduleOperation in moduleOperations)
        {
            TreeNodeDto item = new TreeNodeDto();
            item.Key = $"{moduleOperation.ModuleId}-{moduleOperation.OperationId}";
            item.Label = moduleOperation.Operation.Name;
            item.Children = new List<TreeNodeDto>();
            items.Add(item);
        }
        return items;
    }
}