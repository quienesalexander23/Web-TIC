---
name: Academic Precision
colors:
  surface: '#f8f9fa'
  surface-dim: '#d9dadb'
  surface-bright: '#f8f9fa'
  surface-container-lowest: '#ffffff'
  surface-container-low: '#f3f4f5'
  surface-container: '#edeeef'
  surface-container-high: '#e7e8e9'
  surface-container-highest: '#e1e3e4'
  on-surface: '#191c1d'
  on-surface-variant: '#424751'
  inverse-surface: '#2e3132'
  inverse-on-surface: '#f0f1f2'
  outline: '#737783'
  outline-variant: '#c2c6d3'
  surface-tint: '#255dad'
  primary: '#00346f'
  on-primary: '#ffffff'
  primary-container: '#004a99'
  on-primary-container: '#9bbdff'
  inverse-primary: '#abc7ff'
  secondary: '#b6171e'
  on-secondary: '#ffffff'
  secondary-container: '#da3433'
  on-secondary-container: '#fffbff'
  tertiary: '#1f3944'
  on-tertiary: '#ffffff'
  tertiary-container: '#36505b'
  on-tertiary-container: '#a6c1cf'
  error: '#ba1a1a'
  on-error: '#ffffff'
  error-container: '#ffdad6'
  on-error-container: '#93000a'
  primary-fixed: '#d7e2ff'
  primary-fixed-dim: '#abc7ff'
  on-primary-fixed: '#001b3f'
  on-primary-fixed-variant: '#00458f'
  secondary-fixed: '#ffdad6'
  secondary-fixed-dim: '#ffb3ac'
  on-secondary-fixed: '#410003'
  on-secondary-fixed-variant: '#930010'
  tertiary-fixed: '#cbe7f5'
  tertiary-fixed-dim: '#afcbd8'
  on-tertiary-fixed: '#021f29'
  on-tertiary-fixed-variant: '#304a55'
  background: '#f8f9fa'
  on-background: '#191c1d'
  surface-variant: '#e1e3e4'
typography:
  h1:
    fontFamily: Inter
    fontSize: 40px
    fontWeight: '700'
    lineHeight: '1.2'
    letterSpacing: -0.02em
  h2:
    fontFamily: Inter
    fontSize: 32px
    fontWeight: '600'
    lineHeight: '1.25'
    letterSpacing: -0.01em
  h3:
    fontFamily: Inter
    fontSize: 24px
    fontWeight: '600'
    lineHeight: '1.3'
    letterSpacing: '0'
  body-lg:
    fontFamily: Inter
    fontSize: 18px
    fontWeight: '400'
    lineHeight: '1.6'
    letterSpacing: '0'
  body-md:
    fontFamily: Inter
    fontSize: 16px
    fontWeight: '400'
    lineHeight: '1.5'
    letterSpacing: '0'
  body-sm:
    fontFamily: Inter
    fontSize: 14px
    fontWeight: '400'
    lineHeight: '1.4'
    letterSpacing: '0'
  label-caps:
    fontFamily: Inter
    fontSize: 12px
    fontWeight: '700'
    lineHeight: '1'
    letterSpacing: 0.05em
  button:
    fontFamily: Inter
    fontSize: 14px
    fontWeight: '600'
    lineHeight: '1'
    letterSpacing: 0.01em
rounded:
  sm: 0.125rem
  DEFAULT: 0.25rem
  md: 0.375rem
  lg: 0.5rem
  xl: 0.75rem
  full: 9999px
spacing:
  base: 4px
  xs: 4px
  sm: 8px
  md: 16px
  lg: 24px
  xl: 40px
  gutter: 24px
  margin: 32px
  max-width: 1280px
---

## Brand & Style

This design system is built upon the heritage of the Escuela Politécnica Nacional (EPN), emphasizing academic excellence, technical rigor, and institutional stability. The visual language is **Corporate Modern with Minimalist influences**, prioritizing clarity of information and ease of navigation for faculty, students, and researchers.

The aesthetic is characterized by significant white space, a disciplined color palette, and a structured layout that reflects the precision of a technical institution. It avoids decorative excess in favor of functional elegance, ensuring that the WebTIC FIS platform feels like a reliable extension of the university's physical infrastructure.

## Colors

The color strategy uses **Institutional Blue** as the foundational anchor for navigation, headers, and primary actions, symbolizing trust and authority. **EPN Red** is reserved specifically for high-impact interactions, alerts, and critical status indicators to maintain its visual potency without overwhelming the user.

The background system relies on a high-contrast white base with **Surface Gray (#F8F9FA)** used for grounding layout sections and providing subtle differentiation between content modules. Accent colors are kept to a minimum, primarily using grayscale and muted blues to maintain a professional atmosphere.

## Typography

This design system utilizes **Inter** for its exceptional legibility and systematic feel. The typographic scale is optimized for information-dense interfaces. Headlines use a tighter tracking and heavier weight to provide clear visual hierarchy, while body text prioritizes a generous line height to ensure comfortable reading of long-form academic documentation or administrative reports. Small labels are frequently capitalized to distinguish them as metadata or structural indicators.

## Layout & Spacing

The layout follows a **Fixed Grid** system for desktop views, centered on a 1280px container to ensure readability and maintain the "academic paper" feel. A standard 12-column grid is used with 24px gutters.

Spacing is based on a 4px baseline grid. Elements should be grouped using logical proximity; for example, related form fields use `sm` spacing, while distinct sections of a page use `xl` spacing to provide clear visual breathing room. Margins are generous at 32px to ensure content does not feel cramped against the viewport edges.

## Elevation & Depth

To maintain a clean, professional aesthetic, this design system uses **low-contrast outlines** and **ambient shadows**. 

- **Level 0 (Flat):** Used for the main background.
- **Level 1 (Subtle Outline):** Used for cards and input fields. A 1px border in `#E0E0E0` defines the shape without adding visual weight.
- **Level 2 (Soft Shadow):** Used for hovered states or primary navigation bars. Shadows are highly diffused (12px-16px blur) with very low opacity (5-8%) and no color tinting.
- **Depth Tiering:** Depth is primarily communicated through tonal changes in the background (White vs. Light Gray) rather than aggressive shadows, preserving a flat, modern academic look.

## Shapes

The shape language is conservative and disciplined. A **Soft (0.25rem)** border-radius is applied to buttons, inputs, and cards. This slight rounding softens the technical nature of the site without appearing overly "bubbly" or informal. For larger containers like cards, a `rounded-lg` (0.5rem) may be used to provide a clear distinction from the outer layout, but sharp corners are preferred over high-radius curves to maintain a serious, institutional tone.

## Components

### Institutional Branding
The header must always feature the **EPN Logo** on the far left, followed by a vertical separator and the **FIS (Facultad de Ingeniería de Sistemas)** identifier. The logo should be placed on a white or primary blue background for maximum legibility.

### Buttons
- **Primary:** Institutional Blue background, white text, 4px border radius.
- **Secondary:** White background, Institutional Blue border (1px), Blue text.
- **Destructive:** EPN Red background, white text, used sparingly for deletions or critical errors.

### Input Fields
Inputs use a white background with a 1px `#E0E0E0` border. On focus, the border shifts to Institutional Blue with a subtle 2px outer glow. Labels are positioned above the field in `body-sm` bold.

### Cards
Cards are the primary container for information modules. They feature a white background, a 1px light gray border, and no shadow in their default state. Upon hover, a Level 2 soft shadow is applied to indicate interactivity.

### Status Indicators
Academic or administrative status (e.g., "Approved", "Pending", "Rejected") should use small, square-shouldered chips with light tinted backgrounds and dark text to ensure high contrast and professional appearance.

### Navigation
The sidebar or top navigation utilizes Institutional Blue for the active state indicator. Hover states should use a very light blue tint or a subtle background shift to `#F8F9FA`.