# CSS Architecture with SMACSS

This project's CSS has been organized following the SMACSS (Scalable and Modular Architecture for CSS) methodology. This approach makes the styles more maintainable, scalable, and easier to understand.

## SMACSS Structure

The CSS is organized into these categories:

### 1. Base
Located in `/src/styles/base/index.css`

Contains:
- CSS resets
- Typography rules
- CSS variables
- Default styles for HTML elements
- Box-sizing rules

### 2. Layout
Located in `/src/styles/layout/index.css`

Contains:
- Major layout components (header, footer, main content)
- Grid systems and structural components
- Responsive layout adjustments
- Page layout structures

### 3. Modules
Located in `/src/styles/modules/`

Contains:
- Reusable UI components
- Component styles like buttons, cards, tables
- Individual module files for complex components (e.g., PropertyTraces.css)

### 4. State
Located in `/src/styles/state/index.css`

Contains:
- State-specific styles like hover, active, expanded
- Hidden/visible states
- Form validation states
- Loading/error states

### 5. Theme
Located in `/src/styles/theme/index.css`

Contains:
- Theme variables
- Color schemes
- Dark mode/light mode settings
- Theme-specific overrides

## Import Structure

All styles are imported in the correct cascade order through `/src/styles/index.css`:

1. Theme - Defines variables
2. Base - Defines base styles
3. Layout - Defines structural components
4. Modules - Defines reusable components
5. State - Defines component states

## Benefits of This Structure

- **Modularity**: Easier to maintain and update specific components
- **Organization**: Clear separation of concerns
- **Reusability**: Components are self-contained
- **Scalability**: New features can be added without affecting existing styles
- **Readability**: Easier for developers to find and understand code
- **Consistency**: Enforces consistent naming and organization patterns