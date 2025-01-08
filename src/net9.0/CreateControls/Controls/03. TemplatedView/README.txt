Pros & Cons

Pros
- Simple to create. No platform-specific knowledge required; everything is built using the .NET MAUI abstraction.
- Define the control only once for all platforms.
- It allows not only to customize the control via properties, but also to access the template and modify anything!

Cons
- If to define a control, we create it via composition using 5 .NET MAUI views, it is required to instantiate those 5 views with a performance impact.