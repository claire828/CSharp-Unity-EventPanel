

# Deposit Events in IOC (Client)
A feature from a app game I built in C#, Unity.

This is a dinamic UI-feature (Event Panel) as a Layout controler serves for many different eventPanel(features). Each feature has its own behavior serve as layout content mutiple nest features.

The strucutre is based on IOC & Factory Pattern. 

Most of codes were takeoff due to sensitive infomation.

<br />

### Structure
Below is the simplified version of the application structure.
```
.
└── root
    │ 
    └── Controller.cs  (Entry, Feature/NestFeature Generator / register)
    └── Btn.cs
    │ 
    └── Base (dir) 
        │ ** abstruct logic be extended / implemented by feature panel **
        │ 
        └── BaseMutiplePanel.cs (abstruct) -> logic(UI / User interactive)
        └── IEventPage.cs (interface) -> behavior and def
        └── IComDeposit.cs (interface) -> for one of nest-panel
        │ 
        │   
        └── Panels (dir)
            │ 
            ├── Common
            │   ├── ComDeposit - main UI
            │   └── ComImp (interface for nest features )
            │ 
            ├── Feature Type A - uniq
            │   ├── panel.cs
            │   └── ...cs
            │       
            │ 
            └── Feature Type B - uniq
                
```



## Demo
![demo](/assets/example/sp.png)
![demo](/assets/example/friend.jpg)
![demo](/assets/example/topup.jpg)


<br />
