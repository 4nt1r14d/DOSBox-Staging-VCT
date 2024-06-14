﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dosbox_staging_vct
{
    internal class GlobalConf
    {
        #region Why this class is different from UserConf
        /*
        Both classes are different because all properties in globalConf are mandatory
        instead userConf that are optional.

        If, for any reason, a property from global conf file is not present, this  program
        will replace it with its default value and DosBox-Stagging will will do the same but
        in the global conf file.
        */
        #endregion

        #region Configuration file for DosBox Staging 0.81.0
        /*
        # This is the configuration file for dosbox-staging (0.81.0).
        # Lines starting with a '#' character are comments.
        */
        #endregion

        #region [sdl]
        /*
        [sdl]
        #          fullscreen: Start directly in fullscreen (disabled by default).
        #                      Run INTRO and see Special Keys for window control hotkeys.
        #             display: Number of display to use; values depend on OS and user settings (0 by default).
        #      fullresolution: What resolution to use for fullscreen: 'original', 'desktop'
        #                      or a fixed size, e.g. 1024x768 ('desktop' by default).
        #    windowresolution: Set intial window size for windowed mode. You can still resize the window
        #                      after startup.
        #                        default:   Select the best option based on your environment and other
        #                                   settings (such as whether aspect ratio correction is enabled).
        #                        small, medium, large (s, m, l):
        #                                   Size the window relative to the desktop.
        #                        WxH:       Specify window size in WxH format in logical units
        #                                   (e.g., 1024x768).
        #     window_position: Set initial window position for windowed mode:
        #                        auto:      Let the window manager decide the position (default).
        #                        X,Y:       Set window position in X,Y format (e.g., 250,100).
        #                                   0,0 is the top-left corner of the screen.
        #  window_decorations: Enable window decorations in windowed mode (enabled by default).
        #        transparency: Set the transparency of the DOSBox Staging screen (0 by default).
        #                      From 0 (no transparency) to 90 (high transparency).
        #           host_rate: Set the host's refresh rate:
        #                        auto:      Use SDI rates, or VRR rates when in fullscreen on a high-refresh
        #                                   rate display (default).
        #                        sdi:       Use serial device interface (SDI) rates, without further
        #                                   adjustment.
        #                        vrr:       Deduct 3 Hz from the reported rate (best practice for VRR
        #                                   displays).
        #                        N:         Specify custom refresh rate in Hz (decimal values are allowed;
        #                                   23.000 is the allowed minimum).
        #               vsync: Set the host video driver's vertical synchronization (vsync) mode:
        #                        auto:      Limit vsync to beneficial cases, such as when using an
        #                                   interpolating VRR display in fullscreen (default).
        #                        on:        Enable vsync. This can prevent tearing in some games but will
        #                                   impact performance or drop frames when the DOS rate exceeds the
        #                                   host rate (e.g. 70 Hz DOS rate vs 60 Hz host rate).
        #                        adaptive:  Enables vsync when the frame rate is higher than the host rate,
        #                                   but disables it when the frame rate drops below the host rate.
        #                                   This is a reasonable alternative on macOS instead of 'on'.
        #                                   Note: only valid in OpenGL output modes; otherwise treated as
        #                                   'on'.
        #                        off:       Attempt to disable vsync to allow quicker frame presentation at
        #                                   the risk of tearing in some games.
        #                        yield:     Let the host's video driver control video synchronization.
        #                      Possible values: auto, on, adaptive, off, yield.
        #          vsync_skip: Number of microseconds to allow rendering to block before skipping the
        #                      next frame. For example, a value of 7000 is roughly half the frame time
        #                      at 70 Hz. 0 disables this and will always render (default).
        #   presentation_mode: Select the frame presentation mode:
        #                        auto:  Intelligently time and drop frames to prevent emulation stalls,
        #                               based on host and DOS frame rates (default).
        #                        cfr:   Always present DOS frames at a constant frame rate.
        #                        vfr:   Always present changed DOS frames at a variable frame rate.
        #                      Possible values: auto, cfr, vfr.
        #              output: Video system to use for output ('opengl' by default).
        #                      'texture' and 'opengl' use bilinear interpolation, 'texturenb' and
        #                      'openglnb' use nearest-neighbour (no-bilinear). Some shaders require
        #                      bilinear interpolation, making that the safest choice.
        #                      Possible values: texture, texturenb, opengl, openglnb.
        #                      Deprecated values: openglpp, surface, texturepp.
        #    texture_renderer: Render driver to use in 'texture' output mode ('auto' by default).
        #                      Use 'texture_renderer = auto' for an automatic choice.
        #                      Possible values: auto, direct3d, direct3d11, direct3d12, opengl, opengles2, software.
        #         waitonerror: Keep the console open if an error has occurred (enabled by default).
        #            priority: Priority levels to apply when active and inactive, respectively.
        #                      ('auto auto' by default)
        #                      'auto' lets the host operating system manage the priority.
        #                      Possible values: auto, lowest, lower, normal, higher, highest.
        #  mute_when_inactive: Mute the sound when the window is inactive (disabled by default).
        # pause_when_inactive: Pause emulation when the window is inactive (disabled by default).
        #          mapperfile: Path to the mapper file ('mapper-sdl2-XYZ.map' by default, where XYZ is the
        #                      current version). Pre-configured maps are bundled in 'resources/mapperfiles'.
        #                      These can be loaded by name, e.g., with 'mapperfile = xbox/xenon2.map'.
        #                      Note: The '--resetmapper' command line option only deletes the default mapper
        #                            file.
        #         screensaver: Use 'allow' or 'block' to override the SDL_VIDEO_ALLOW_SCREENSAVER environment
        #                      variable which usually blocks the OS screensaver while the emulator is
        #                      running ('auto' by default).
        #                      Possible values: auto, allow, block.

        fullscreen          = false
        display             = 0
        fullresolution      = desktop
        windowresolution    = default
        window_position     = auto
        window_decorations  = true
        transparency        = 0
        host_rate           = auto
        vsync               = auto
        vsync_skip          = 0
        presentation_mode   = auto
        output              = opengl
        texture_renderer    = auto
        waitonerror         = true
        priority            = auto auto
        mute_when_inactive  = false
        pause_when_inactive = false
        mapperfile          = mapper-sdl2-0.81.0.map
        screensaver         = auto
        */
        #endregion
        public string SDL_fullscreen { get; set; } = "false";
        public string SDL_display { get; set; } = "0";
        public string SDL_fullresolution { get; set; } = "desktop";
        public string SDL_windowresolution { get; set; } = "default";
        public string SDL_window_position { get; set; } = "auto";
        public string SDL_window_decorations { get; set; } = "true";
        public string SDL_transparency { get; set; } = "0";
        public string SDL_host_rate { get; set; } = "auto";
        public string SDL_vsync { get; set; } = "auto";
        public string SDL_vsync_skip { get; set; } = "0";
        public string SDL_presentation_mode { get; set; } = "auto";
        public string SDL_output { get; set; } = "opengl";
        public string SDL_texture_renderer { get; set; } = "auto";
        public string SDL_waitonerror { get; set; } = "true";
        public string SDL_priority { get; set; } = "auto auto";
        public string SDL_mute_when_inactive { get; set; } = "false";
        public string SDL_pause_when_inactive { get; set; } = "false";
        public string SDL_mapperfile { get; set; } = "mapper-sdl2-0.81.0.map";
        public string SDL_screensaver { get; set; } = "auto";

        #region [dosbox]
        /*
        [dosbox]
        #                    language: Select a language to use: 'br', 'de', 'en', 'es', 'fr', 'it', 'nl', 'pl',
        #                              or 'ru' (unset by default; this defaults to English).
        #                              Notes:
        #                                - This setting will override the 'LANG' environment variable, if set.
        #                                - The bundled 'resources/translations' directory with the executable holds
        #                                  these files. Please keep it along-side the executable to support this
        #                                  feature.
        #                     machine: Set the video adapter or machine to emulate:
        #                                hercules:       Hercules Graphics Card (HGC) (see 'monochrome_palette').
        #                                cga_mono:       CGA adapter connected to a monochrome monitor (see
        #                                                'monochrome_palette').
        #                                cga:            IBM Color Graphics Adapter (CGA). Also enables composite
        #                                                video emulation (see [composite] section).
        #                                pcjr:           An IBM PCjr machine. Also enables PCjr sound and composite
        #                                                video emulation (see [composite] section).
        #                                tandy:          A Tandy 1000 machine with TGA graphics. Also enables Tandy
        #                                                sound and composite video emulation (see [composite]
        #                                                section).
        #                                ega:            IBM Enhanced Graphics Adapter (EGA).
        #                                svga_paradise:  Paradise PVGA1A SVGA card (no VESA VBE; 512K vmem by default,
        #                                                can be set to 256K or 1MB with 'vmemsize'). This is the
        #                                                closest to IBM's original VGA adapter.
        #                                svga_et3000:    Tseng Labs ET3000 SVGA card (no VESA VBE; fixed 512K vmem).
        #                                svga_et4000:    Tseng Labs ET4000 SVGA card (no VESA VBE; 1MB vmem by
        #                                                default, can be set to 256K or 512K with 'vmemsize').
        #                                svga_s3:        S3 Trio64 (VESA VBE 2.0; 4MB vmem by default, can be set to
        #                                                512K, 1MB, 2MB, or 8MB with 'vmemsize') (default)
        #                                vesa_oldvbe:    Same as 'svga_s3' but limited to VESA VBE 1.2.
        #                                vesa_nolfb:     Same as 'svga_s3' (VESA VBE 2.0), plus the "no linear
        #                                                framebuffer" hack (needed only by a few games).
        #                              Possible values: hercules, cga_mono, cga, pcjr, tandy, ega, svga_s3, svga_et3000, svga_et4000, svga_paradise, vesa_nolfb, vesa_oldvbe.
        #                              Deprecated values: vgaonly.
        #                     memsize: Amount of memory of the emulated machine has in MB (16 by default).
        #                              Best leave at the default setting to avoid problems with some games,
        #                              though a few games might require a higher value.
        #                              There is generally no speed advantage when raising this value.
        #          mcb_fault_strategy: How software-corrupted memory chain blocks should be handled:
        #                                repair:  Repair (and report) faults using adjacent blocks (default).
        #                                report:  Report faults but otherwise proceed as-is.
        #                                allow:   Allow faults to go unreported (hardware behavior).
        #                                deny:    Quit (and report) when faults are detected.
        #                              Possible values: repair, report, allow, deny.
        #                    vmemsize: Video memory in MB (1-8) or KB (256 to 8192). 'auto' uses the default for
        #                              the selected video adapter ('auto' by default). See the 'machine' setting for
        #                              the list of valid options per adapter.
        #                              Possible values: auto, 1, 2, 4, 8, 256, 512, 1024, 2048, 4096, 8192.
        #                  vmem_delay: Set video memory access delay emulation ('off' by default).
        #                                off:      Disable video memory access delay emulation (default).
        #                                          This is preferable for most games to avoid slowdowns.
        #                                on:       Enable video memory access delay emulation (3000 ns).
        #                                          This can help reduce or eliminate flicker in Hercules,
        #                                          CGA, EGA, and early VGA games.
        #                                <value>:  Set access delay in nanoseconds. Valid range is 0 to 20000 ns;
        #                                          500 to 5000 ns is the most useful range.
        #                              Note: Only set this on a per-game basis when necessary as it slows down
        #                                    the whole emulator.
        #                    dos_rate: Customize the emulated video mode's frame rate.
        #                                default:  The DOS video mode determines the rate (default).
        #                                host:     Match the DOS rate to the host rate (see 'host_rate' setting).
        #                                <value>:  Sets the rate to an exact value in between 24.000 and 1000.000 Hz.
        #                              Note: We recommend the 'default' rate, otherwise test and set on a per-game
        #                                    basis.
        #                  vesa_modes: Controls the selection of VESA 1.2 and 2.0 modes offered:
        #                                compatible:  A tailored selection that maximizes game compatibility.
        #                                             This is recommended along with 4 or 8 MB of video memory
        #                                             (default).
        #                                halfline:    Supports the low-resolution halfline VESA 2.0 mode used by
        #                                             Extreme Assault. Use only if needed, as it's not S3 compatible.
        #                                all:         All modes for a given video memory size, however some games may
        #                                             not use them properly (flickering) or may need more system
        #                                             memory to use them.
        #                              Possible values: compatible, all, halfline.
        #               vga_8dot_font: Use 8-pixel-wide fonts on VGA adapters (disabled by default).
        #     vga_render_per_scanline: Emulate accurate per-scanline VGA rendering (enabled by default).
        #                              Currently, you need to disable this for a few games, otherwise they will crash
        #                              at startup (e.g., Deus, Ishar 3, Robinson's Requiem, Time Warriors).
        #                  speed_mods: Permit changes known to improve performance (enabled by default).
        #                              Currently, no games are known to be negatively affected by this.
        #                              Please file a bug with the project if you find a game that fails
        #                              when this is enabled so we will list them here.
        #            autoexec_section: How autoexec sections are handled from multiple config files:
        #                                join:       Combine them into one big section (legacy behavior; default).
        #                                overwrite:  Use the last one encountered, like other config settings.
        #                              Possible values: join, overwrite.
        #                   automount: Mount 'drives/[c]' directories as drives on startup, where [c] is a lower-case
        #                              drive letter from 'a' to 'y' (enabled by default). The 'drives' folder can be
        #                              provided relative to the current directory or via built-in resources.
        #                              Mount settings can be optionally provided using a [c].conf file along-side
        #                              the drive's directory, with content as follows:
        #                                [drive]
        #                                type    = dir, overlay, floppy, or cdrom
        #                                label   = custom_label
        #                                path    = path-specification, ie: path = %%path%%;c:\tools
        #                                override_drive = mount the directory to this drive instead (default empty)
        #                                verbose = true or false
        #           startup_verbosity: Controls verbosity prior to displaying the program ('auto' by default):
        #                                Verbosity   | Welcome | Early stdout
        #                                high        |   yes   |    yes
        #                                low         |   no    |    yes
        #                                quiet       |   no    |    no
        #                                auto        | 'low' if exec or dir is passed, otherwise 'high'
        #                              Possible values: auto, high, low, quiet.
        # allow_write_protected_files: Many games open all their files with writable permissions; even files that they
        #                              never modify. This setting lets you write-protect those files while still
        #                              allowing the game to read them (enabled by default). A second use-case: if
        #                              you're using a copy-on-write or network-based filesystem, this setting avoids
        #                              triggering write operations for these write-protected files.
        #      shell_config_shortcuts: Allow shortcuts for simpler configuration management (enabled by default).
        #                              E.g., instead of 'config -set sbtype sb16', it is enough to execute
        #                              'sbtype sb16', and instead of 'config -get sbtype', you can just execute
        #                              the 'sbtype' command.

        language                    = 
        machine                     = svga_s3
        memsize                     = 16
        mcb_fault_strategy          = repair
        vmemsize                    = auto
        vmem_delay                  = off
        dos_rate                    = default
        vesa_modes                  = compatible
        vga_8dot_font               = false
        vga_render_per_scanline     = true
        speed_mods                  = true
        autoexec_section            = join
        automount                   = true
        startup_verbosity           = auto
        allow_write_protected_files = true
        shell_config_shortcuts      = true
         */
        #endregion
        public string DOSBOX_language { get; set; } = "";
        public string DOSBOX_machine { get; set; } = "svga_s3";
        public string DOSBOX_memsize { get; set; } = "16";
        public string DOSBOX_mcb_fault_strategy { get; set; } = "repair";
        public string DOSBOX_vmemsize { get; set; } = "auto";
        public string DOSBOX_vmem_delay { get; set; } = "off";
        public string DOSBOX_dos_rate { get; set; } = "default";
        public string DOSBOX_vesa_modes { get; set; } = "compatible";
        public string DOSBOX_vga_8dot_font { get; set; } = "false";
        public string DOSBOX_vga_render_per_scanline { get; set; } = "true";
        public string DOSBOX_speed_mods { get; set; } = "true";
        public string DOSBOX_autoexec_section { get; set; } = "join";
        public string DOSBOX_automount { get; set; } = "true";
        public string DOSBOX_startup_verbosity { get; set; } = "auto";
        public string DOSBOX_allow_write_protected_files { get; set; } = "true";
        public string DOSBOX_shell_config_shortcuts { get; set; } = "true";

        #region [render]
        /*
        [render]
        #             aspect: Set the aspect ratio correction mode (enabled by default):
        #                       auto, on:            Apply aspect ratio correction for modern square-pixel
        #                                            flat-screen displays, so DOS video modes with non-square
        #                                            pixels appear as they would on a 4:3 display aspect
        #                                            ratio CRT monitor the majority of DOS games were
        #                                            designed for. This setting only affects video modes that
        #                                            use non-square pixels, such as 320x200 or 640x400;.
        #                                            square-pixelmodes (e.g., 320x240, 640x480, and 800x600),
        #                                            are displayed as-is.
        #                       square-pixels, off:  Don't apply aspect ratio correction; all DOS video modes
        #                                            are displayed with square pixels. Most 320x200 games
        #                                            will appear squashed, but a minority of titles (e.g.,
        #                                            DOS ports of PAL Amiga games) need square pixels to
        #                                            appear as the artists intended.
        #                       stretch:             Calculate the aspect ratio from the viewport's
        #                                            dimensions. Combined with 'viewport', this mode is
        #                                            useful to force arbitrary aspect ratios (e.g.,
        #                                            stretching DOS games to fullscreen on 16:9 displays) and
        #                                            to emulate the horizontal and vertical stretch controls
        #                                            of CRT monitors.
        #                     Possible values: auto, on, square-pixels, off, stretch.
        #    integer_scaling: Constrain the horizontal or vertical scaling factor to the largest integer
        #                     value so the image still fits into the viewport. The configured aspect ratio is
        #                     always maintained according to the 'aspect' and 'viewport' settings, which may
        #                     result in a non-integer scaling factor in the other dimension. If the image is
        #                     larger than the viewport, the integer scaling constraint is auto-disabled (same
        #                     as 'off'). Possible values:
        #                       auto:        'vertical' mode auto-enabled for adaptive CRT shaders only
        #                                    (see 'glshader'), otherwise 'off' (default).
        #                       vertical:    Constrain the vertical scaling factor to integer values.
        #                                    This is the recommended setting for CRT shaders to avoid uneven
        #                                    scanlines and interference artifacts.
        #                       horizontal:  Constrain the horizontal scaling factor to integer values.
        #                       off:         No integer scaling constraint is applied; the image fills the
        #                                    viewport while maintaining the configured aspect ratio.
        #                     Possible values: auto, vertical, horizontal, off.
        #           viewport: Set the viewport size (maximum drawable area). The video output is always
        #                     contained within the viewport while taking the configured aspect ratio into
        #                     account (see 'aspect'). Possible values:
        #                       fit:             Fit the viewport into the available window/screen (default).
        #                                        There might be padding (black areas) around the image with
        #                                        'integer_scaling' enabled.
        #                       WxH:             Set a fixed viewport size in WxH format in logical units
        #                                        (e.g., 960x720). The specified size must not be larger than
        #                                        the desktop. If it's larger than the window size, it's
        #                                        scaled to fit within the window.
        #                       N%:              Similar to 'WxH' but the size is specified as a percentage
        #                                        of the desktop size.
        #                       relative H% V%:  The viewport is set to a 4:3 aspect ratio rectangle fit into
        #                                        the available window/screen, then it's scaled by the H and V
        #                                        horizontal and vertical scaling factors (valid range is from
        #                                        20% to 300%). The resulting viewport is allowed to extend
        #                                        beyond the window/screen. Useful to force arbitrary display
        #                                        aspect ratios with 'aspect = stretch' and to zoom into the
        #                                        image. This effectively emulates the horizontal and vertical
        #                                        stretch controls of CRT monitors.
        #                     Notes:
        #                       - Using 'relative' mode with 'integer_scaling' enabled could lead to
        #                         surprising (but correct) results.
        #                       - You can use the 'Stretch Axis', 'Inc Stretch', and 'Dec Stretch' hotkey
        #                         actions to set the stretch in 'relative' mode in real-time.
        # monochrome_palette: Set the palette for monochrome display emulation ('amber' by default).
        #                     Works only with the 'hercules' and 'cga_mono' machine types.
        #                     Note: You can also cycle through the available palettes via hotkeys.
        #                     Possible values: amber, green, white, paperwhite.
        #         cga_colors: Set the interpretation of CGA RGBI colours. Affects all machine types capable
        #                     of displaying CGA or better graphics. Built-in presets:
        #                       default:       The canonical CGA palette, as emulated by VGA adapters
        #                                      (default).
        #                       tandy <bl>:    Emulation of an idealised Tandy monitor with adjustable brown
        #                                      level. The brown level can be provided as an optional second
        #                                      parameter (0 - red, 50 - brown, 100 - dark yellow;
        #                                      defaults to 50). E.g. tandy 100
        #                       tandy-warm:    Emulation of the actual colour output of an unknown Tandy
        #                                      monitor.
        #                       ibm5153 <c>:   Emulation of the actual colour output of an IBM 5153 monitor
        #                                      with a unique contrast control that dims non-bright colours
        #                                      only. The contrast can be optionally provided as a second
        #                                      parameter (0 to 100; defaults to 100), e.g. ibm5153 60
        #                       agi-amiga-v1, agi-amiga-v2, agi-amiga-v3:
        #                                      Palettes used by the Amiga ports of Sierra AGI games.
        #                       agi-amigaish:  A mix of EGA and Amiga colours used by the Sarien
        #                                      AGI-interpreter.
        #                       scumm-amiga:   Palette used by the Amiga ports of LucasArts EGA games.
        #                       colodore:      Commodore 64 inspired colours based on the Colodore palette.
        #                       colodore-sat:  Colodore palette with 20% more saturation.
        #                       dga16:         A modern take on the canonical CGA palette with dialed back
        #                                      contrast.
        #                     You can also set custom colours by specifying 16 space or comma separated
        #                     colour values, either as 3 or 6-digit hex codes (e.g. #f00 or #ff0000 for full
        #                     red), or decimal RGB triplets (e.g. (255, 0, 255) for magenta). The 16 colours
        #                     are ordered as follows:
        #                       black, blue, green, cyan, red, magenta, brown, light-grey, dark-grey,
        #                       light-blue, light-green, light-cyan, light-red, light-magenta, yellow, white.
        #                     Their default values, shown here in 6-digit hex code format, are:
        #                       #000000 #0000aa #00aa00 #00aaaa #aa0000 #aa00aa #aa5500 #aaaaaa
        #                       #555555 #5555ff #55ff55 #55ffff #ff5555 #ff55ff #ffff55 #ffffff
        #           glshader: Set an adaptive CRT monitor emulation shader or a regular GLSL shader in OpenGL
        #                     output modes. Adaptive CRT shader options:
        #                       crt-auto:               A CRT shader that prioritises developer intent and
        #                                               how people experienced the game at the time of
        #                                               release (default). The appropriate shader variant is
        #                                               automatically selected based the graphics standard of
        #                                               the current video mode and the viewport size,
        #                                               irrespective of the 'machine' setting. This means
        #                                               that even on an emulated VGA card you'll get
        #                                               authentic single-scanned EGA monitor emulation with
        #                                               visible "thick scanlines" in EGA games.
        #                       crt-auto-machine:       Similar to 'crt-auto', but this picks a fixed CRT
        #                                               monitor appropriate for the video adapter configured
        #                                               via the 'machine' setting. E.g., CGA and EGA games
        #                                               will appear double-scanned on an emulated VGA
        #                                               adapter.
        #                       crt-auto-arcade:        Emulation of an arcade or home computer monitor less
        #                                               sharp than a typical PC monitor with thick scanlines
        #                                               in low-resolution modes. This fantasy option does not
        #                                               exist in real life, but it can be a lot of fun,
        #                                               especially with DOS ports of Amiga games.
        #                       crt-auto-arcade-sharp:  A sharper variant of the arcade shader for those who
        #                                               like the thick scanlines but want to retain the
        #                                               horizontal sharpness of a typical PC monitor.
        #                     Other options include 'sharp', 'none', a shader listed using the
        #                     '--list-glshaders' command-line argument, or an absolute or relative path
        #                     to a file. In all cases, you may omit the shader's '.glsl' file extension.

        aspect             = auto
        integer_scaling    = auto
        viewport           = fit
        monochrome_palette = amber
        cga_colors         = default
        glshader           = crt-auto
        */
        #endregion
        public string RENDER_aspect { get; set; } = "auto";
        public string RENDER_integer_scaling { get; set; } = "auto";
        public string RENDER_viewport { get; set; } = "fit";
        public string RENDER_monochrome_palette { get; set; } = "amber";
        public string RENDER_cga_colors { get; set; } = "default";
        public string RENDER_glshader { get; set; } = "glshader";

        #region [composite]
        /*
        [composite]
        #   composite: Enable composite mode on start (only for 'cga', 'pcjr', and 'tandy' machine
        #              types; 'auto' by default). 'auto' lets the program decide.
        #              Note: Fine-tune the settings below (i.e., 'hue') using the composite hotkeys,
        #                    then copy the new settings from the logs into your config.
        #              Possible values: auto, on, off.
        #         era: Era of composite technology ('auto' by default).
        #              When 'auto', PCjr uses 'new', and CGA/Tandy use 'old'.
        #              Possible values: auto, old, new.
        #         hue: Hue of the RGB palette (0 by default).
        #              For example, adjust until the sky is blue.
        #  saturation: Intensity of colors, from washed out to vivid (100 by default).
        #    contrast: Ratio between the dark and light area (100 by default).
        #  brightness: Luminosity of the image, from dark to light (0 by default).
        # convergence: Convergence of subpixel elements, from blurry to sharp (0 by default).

        composite   = auto
        era         = auto
        hue         = 0
        saturation  = 100
        contrast    = 100
        brightness  = 0
        convergence = 0 
        */
        #endregion
        public string COMPOSITE_composite { get; set; } = "auto";
        public string COMPOSITE_era { get; set; } = "auto";
        public string COMPOSITE_hue { get; set; } = "0";
        public string COMPOSITE_saturation { get; set; } = "100";
        public string COMPOSITE_contrast { get; set; } = "100";
        public string COMPOSITE_brightness { get; set; } = "0";
        public string COMPOSITE_convergence { get; set; } = "0";

        #region [cpu]
        /*
        [cpu]
        #      core: CPU core used in emulation ('auto' by default). 'auto' will switch to dynamic
        #            if available and appropriate.
        #            Possible values: auto, dynamic, normal, simple.
        #   cputype: CPU type used in emulation ('auto' by default). 'auto' is the fastest choice.
        #            Possible values: auto, 386, 386_slow, 486_slow, pentium_slow, 386_prefetch.
        #    cycles: Number of instructions DOSBox tries to emulate per millisecond
        #            ('auto' by default). Setting this value too high may result in sound drop-outs
        #            and lags.
        #              auto:            Try to guess what a game needs. It usually works, but can
        #                               fail with certain games.
        #              fixed <number>:  Set a fixed number of cycles. This is what you usually
        #                               need if 'auto' fails (e.g. 'fixed 4000').
        #              max:             Allocate as much cycles as your computer is able to handle.
        #            Possible values: auto, fixed, max.
        #   cycleup: Number of cycles added with the increase cycles hotkey (10 by default).
        #            Setting it lower than 100 will be a percentage.
        # cycledown: Number of cycles subtracted with the decrease cycles hotkey (20 by default).
        #            Setting it lower than 100 will be a percentage.

        core      = auto
        cputype   = auto
        cycles    = auto
        cycleup   = 10
        cycledown = 20
         */
        #endregion
        public string CPU_core { get; set; } = "auto";
        public string CPU_cputype { get; set; } = "auto";
        public string CPU_cycles { get; set; } = "auto";
        public string CPU_cycleup { get; set; } = "10";
        public string CPU_cycledown { get; set; } = "20";

        #region [voodoo]
        /*
        [voodoo]
        #                    voodoo: Enable 3dfx Voodoo emulation (enabled by default).
        #            voodoo_memsize: Set the amount of video memory for 3dfx Voodoo graphics, either 4 or 12 MB.
        #                            The memory is used by the Frame Buffer Interface (FBI) and Texture Mapping Unit
        #                            (TMU) as follows:
        #                               4: 2 MB for the FBI and one TMU with 2 MB (default).
        #                              12: 4 MB for the FBI and two TMUs, each with 4 MB.
        #                            Possible values: 4, 12.
        #     voodoo_multithreading: Use threads to improve 3dfx Voodoo performance (enabled by default).
        # voodoo_bilinear_filtering: Use bilinear filtering to emulate the 3dfx Voodoo's texture smoothing effect
        #                            (disabled by default). Only suggested if you have a fast desktop-class CPU, as
        #                            it can impact frame rates on slower systems.

        voodoo                    = true
        voodoo_memsize            = 4
        voodoo_multithreading     = true
        voodoo_bilinear_filtering = false 
        */
        #endregion
        public string VOODOO_voodoo { get; set; } = "true";
        public string VOODOO_voodoo_memsize { get; set; } = "4";
        public string VOODOO_voodoo_multithreading { get; set; } = "true";
        public string VOODOO_voodoo_bilinear_filtering { get; set; } = "false";

        #region [capture]
        /*
        [capture]
        #                   capture_dir: Directory where the various captures are saved, such as audio, video, MIDI
        #                                and screenshot captures. ('capture' in the current working directory by
        #                                default).
        # default_image_capture_formats: Set the capture format of the default screenshot action ('upscaled' by
        #                                default):
        #                                  upscaled:  The image is bilinear-sharp upscaled and the correct aspect
        #                                             ratio is maintained, depending on the 'aspect' setting. The
        #                                             vertical scaling factor is always an integer. For example,
        #                                             320x200 content is upscaled to 1600x1200 (5:6 integer scaling),
        #                                             640x480 to 1920x1440 (3:3 integer scaling), and 640x350 to
        #                                             1400x1050 (2.1875:3 scaling; fractional horizontally and
        #                                             integer vertically). The filenames of upscaled screenshots
        #                                             have no postfix (e.g. 'image0001.png').
        #                                  rendered:  The post-rendered, post-shader image shown on the screen is
        #                                             captured. The filenames of rendered screenshots end with
        #                                             '-rendered' (e.g. 'image0001-rendered.png').
        #                                  raw:       The contents of the raw framebuffer is captured (this always
        #                                             results in square pixels). The filenames of raw screenshots
        #                                             end with '-raw' (e.g. 'image0001-raw.png').
        #                                If multiple formats are specified separated by spaces, the default
        #                                screenshot action will save multiple images in the specified formats.
        #                                Keybindings for taking single screenshots in specific formats are also
        #                                available.

        capture_dir                   = capture
        default_image_capture_formats = upscaled
         */
        #endregion
        public string CAPTURE_capture_dir { get; set; } = "capture";
        public string CAPTURE_default_image_capture_formats { get; set; } = "upscaled";

        #region [mouse]
        /*
        [mouse]
        #             mouse_capture: Set the mouse capture behaviour:
        #                              onclick:   Capture the mouse when clicking any mouse button in the window
        #                                         (default).
        #                              onstart:   Capture the mouse immediately on start. Might not work correctly
        #                                         on some host operating systems.
        #                              seamless:  Let the mouse move seamlessly between the DOSBox window and the
        #                                         rest of the desktop; captures only with middle-click or hotkey.
        #                                         Seamless mouse does not work correctly with all the games.
        #                                         Windows 3.1x can be made compatible with a custom mouse driver.
        #                              nomouse:   Hide the mouse and don't send mouse input to the game.
        #                            For touch-screen control, use 'seamless'.
        #                            Possible values: seamless, onclick, onstart, nomouse.
        #      mouse_middle_release: Release the captured mouse by middle-clicking, and also capture it in
        #                            seamless mode (enabled by default).
        # mouse_multi_display_aware: Allow seamless mouse behavior and mouse pointer release to work in fullscreen
        #                            mode on systems with more than one display (enabled by default).
        #                            Note: You should disable this if it incorrectly detects multiple displays
        #                                  when only one should actually be used. This might happen if you are
        #                                  using mirrored display mode or using an AV receiver's HDMI input for
        #                                  audio-only listening.
        #         mouse_sensitivity: Global mouse sensitivity for the horizontal and vertical axes, as a percentage
        #                            (100 by default). Values can be separated by spaces, commas, or semicolons
        #                            (i.e. 100,150). Negative values invert the axis, zero disables it.
        #                            Providing only one value sets sensitivity for both axes.
        #                            Sensitivity can be further fine-tuned per mouse interface using the internal
        #                            MOUSECTL.COM tool available on the Z drive.
        #           mouse_raw_input: Enable to bypass your operating system's mouse acceleration and sensitivity
        #                            settings (enabled by default). Works in fullscreen or when the mouse is
        #                            captured in windowed mode.
        #          dos_mouse_driver: Enable built-in DOS mouse driver (enabled by default).
        #                            Notes:
        #                              - Disable if you intend to use original MOUSE.COM driver in emulated DOS.
        #                              - When guest OS is booted, built-in driver gets disabled automatically.
        #       dos_mouse_immediate: Update mouse movement counters immediately, without waiting for interrupt
        #                            (disabled by default). May improve gameplay, especially in fast-paced games
        #                            (arcade, FPS, etc.), as for some games it effectively boosts the mouse
        #                            sampling rate to 1000 Hz, without increasing interrupt overhead.
        #                            Might cause compatibility issues. List of known incompatible games:
        #                              - Ultima Underworld: The Stygian Abyss
        #                              - Ultima Underworld II: Labyrinth of Worlds
        #                            Please report it if you find another incompatible game so we can update this
        #                            list.
        #           ps2_mouse_model: PS/2 AUX port mouse model:
        #                              standard:      3 buttons, standard PS/2 mouse.
        #                              intellimouse:  3 buttons + wheel, Microsoft IntelliMouse.
        #                              explorer:      5 buttons + wheel, Microsoft IntelliMouse Explorer (default).
        #                              none:          no PS/2 mouse emulated.
        #                            Possible values: standard, intellimouse, explorer, none.
        #           com_mouse_model: COM (serial) port default mouse model:
        #                              2button:      2 buttons, Microsoft mouse.
        #                              3button:      3 buttons, Logitech mouse;
        #                                            mostly compatible with Microsoft mouse.
        #                              wheel:        3 buttons + wheel;
        #                                            mostly compatible with Microsoft mouse.
        #                              msm:          3 buttons, Mouse Systems mouse;
        #                                            NOT compatible with Microsoft mouse.
        #                              2button+msm:  Automatic choice between '2button' and 'msm'.
        #                              3button+msm:  Automatic choice between '3button' and 'msm'.
        #                              wheel+msm:    Automatic choice between 'wheel' and 'msm' (default).
        #                            Note: Enable COM port mice in the [serial] section.
        #                            Possible values: 2button, 3button, wheel, msm, 2button+msm, 3button+msm, wheel+msm.
        #              vmware_mouse: VMware mouse interface (enabled by default).
        #                            Fully compatible only with experimental 3rd party Windows 3.1x driver.
        #                            Note: Requires PS/2 mouse to be enabled.
        #          virtualbox_mouse: VirtualBox mouse interface (enabled by default).
        #                            Fully compatible only with 3rd party Windows 3.1x driver.
        #                            Note: Requires PS/2 mouse to be enabled.

        mouse_capture             = onclick
        mouse_middle_release      = true
        mouse_multi_display_aware = true
        mouse_sensitivity         = 100
        mouse_raw_input           = true
        dos_mouse_driver          = true
        dos_mouse_immediate       = false
        ps2_mouse_model           = explorer
        com_mouse_model           = wheel+msm
        vmware_mouse              = true
        virtualbox_mouse          = true
        */
        #endregion
        public string MOUSE_mouse_capture { get; set; } = "onclick";
        public string MOUSE_mouse_middle_release { get; set; } = "true";
        public string MOUSE_mouse_multi_display_aware { get; set; } = "true";
        public string MOUSE_mouse_sensitivity { get; set; } = "100";
        public string MOUSE_mouse_raw_input { get; set; } = "true";
        public string MOUSE_dos_mouse_driver { get; set; } = "true";
        public string MOUSE_dos_mouse_immediate { get; set; } = "false";
        public string MOUSE_ps2_mouse_model { get; set; } = "explorer";
        public string MOUSE_com_mouse_model { get; set; } = "wheel+msm";
        public string MOUSE_vmware_mouse { get; set; } = "true";
        public string MOUSE_virtualbox_mouse { get; set; } = "true";

        #region [mixer]
        /*
        [mixer]
        #    nosound: Enable silent mode (disabled by default).
        #             Sound is still fully emulated in silent mode, but DOSBox outputs silence.
        #       rate: Mixer sample rate (48000 by default).
        #             Possible values: 8000, 11025, 16000, 22050, 32000, 44100, 48000.
        #  blocksize: Mixer block size in sample frames (1024 by default). Larger values might help
        #             with sound stuttering but the sound will also be more lagged.
        #             Possible values: 128, 256, 512, 1024, 2048, 4096, 8192.
        #  prebuffer: How many milliseconds of sound to render on top of the blocksize
        #             (25 by default). Larger values might help with sound stuttering but the sound
        #             will also be more lagged.
        #  negotiate: Let the system audio driver negotiate possibly better sample rate and blocksize
        #             settings (disabled by default).
        # compressor: Enable the auto-leveling compressor on the master channel to prevent clipping
        #             of the audio output:
        #               off:  Disable compressor.
        #               on:   Enable compressor (default).
        #  crossfeed: Enable crossfeed globally on all stereo channels for headphone listening:
        #               off:     No crossfeed (default).
        #               on:      Enable crossfeed (normal preset).
        #               light:   Light crossfeed (strength 15).
        #               normal:  Normal crossfeed (strength 40).
        #               strong:  Strong crossfeed (strength 65).
        #             Note: You can fine-tune each channel's crossfeed strength using the MIXER.
        #             Possible values: off, on, light, normal, strong.
        #     reverb: Enable reverb globally to add a sense of space to the sound:
        #               off:     No reverb (default).
        #               on:      Enable reverb (medium preset).
        #               tiny:    Simulates the sound of a small integrated speaker in a room;
        #                        specifically designed for small-speaker audio systems
        #                        (PC Speaker, Tandy, PS/1 Audio, and LPT DAC devices).
        #               small:   Adds a subtle sense of space; good for games that use a single
        #                        synth channel (typically OPL) for both music and sound effects.
        #               medium:  Medium room preset that works well with a wide variety of games.
        #               large:   Large hall preset recommended for games that use separate
        #                        channels for music and digital audio.
        #               huge:    A stronger variant of the large hall preset; works really well
        #                        in some games with more atmospheric soundtracks.
        #             Note: You can fine-tune each channel's reverb level using the MIXER.
        #             Possible values: off, on, tiny, small, medium, large, huge.
        #     chorus: Enable chorus globally to add a sense of stereo movement to the sound:
        #               off:     No chorus (default).
        #               on:      Enable chorus (normal preset).
        #               light:   A light chorus effect (especially suited for synth music that
        #                        features lots of white noise).
        #               normal:  Normal chorus that works well with a wide variety of games.
        #               strong:  An obvious and upfront chorus effect.
        #             Note: You can fine-tune each channel's chorus level using the MIXER.
        #             Possible values: off, on, light, normal, strong.

        nosound    = false
        rate       = 48000
        blocksize  = 1024
        prebuffer  = 25
        negotiate  = false
        compressor = true
        crossfeed  = off
        reverb     = off
        chorus     = off
        */
        #endregion
        public string MIXER_nosound { get; set; } = "false";
        public string MIXER_rate { get; set; } = "48000";
        public string MIXER_blocksize { get; set; } = "1024";
        public string MIXER_prebuffer { get; set; } = "25";
        public string MIXER_negotiate { get; set; } = "false";
        public string MIXER_compressor { get; set; } = "true";
        public string MIXER_crossfeed { get; set; } = "off";
        public string MIXER_reverb { get; set; } = "off";
        public string MIXER_chorus { get; set; } = "off";

        #region [midi]
        /*
        [midi]
        #      mididevice: Set where MIDI data from the emulated MPU-401 MIDI interface is sent
        #                  ('auto' by default):
        #                    win32:       Use the Win32 MIDI playback interface.
        #                    fluidsynth:  The built-in FluidSynth MIDI synthesizer (SoundFont player).
        #                                 See the [fluidsynth] section for detailed configuration.
        #                    mt32:        The built-in Roland MT-32 synthesizer.
        #                                 See the [mt32] section for detailed configuration.
        #                    auto:        Either one of the built-in MIDI synthesisers (if `midiconfig` is
        #                                 set to 'fluidsynth' or 'mt32'), or a MIDI device external to
        #                                 DOSBox (any other 'midiconfig' value). This might be a software
        #                                 synthesizer or physical device. This is the default behaviour.
        #                    none:        Disable MIDI output.
        #                  Possible values: auto, win32, fluidsynth, mt32, none.
        #      midiconfig: Configuration options for the selected MIDI interface (unset by default).
        #                  This is usually the ID or name of the MIDI synthesizer you want
        #                  to use (find the ID/name with the DOS command 'MIXER /LISTMIDI').
        #                  Notes:
        #                    - This option has no effect when using the built-in synthesizers
        #                      ('mididevice = fluidsynth' or 'mididevice = mt32').
        #                    - If you're using a physical Roland MT-32 with revision 0 PCB, the hardware
        #                      may require a delay in order to prevent its buffer from overflowing.
        #                      In that case, add 'delaysysex' (e.g. 'midiconfig = 2 delaysysex').
        #          mpu401: MPU-401 mode to emulate ('intelligent' by default).
        #                  Possible values: intelligent, uart, none.
        # raw_midi_output: Enable raw, unaltered MIDI output (disabled by default).
        #                  The MIDI drivers of many games don't fully conform to the MIDI standard,
        #                  which makes editing the MIDI recordings of these games very error-prone and
        #                  cumbersome in MIDI sequencers, often resulting in hanging or missing notes.
        #                  DOSBox corrects the MIDI output of such games by default. This results in no
        #                  audible difference whatsoever; it only affects the representation of the MIDI
        #                  data. You should only enable 'raw_midi_output' if you really need to capture
        #                  the raw, unaltered MIDI output of a program, e.g. when working with music
        #                  applications, or when debugging MIDI issues.

        mididevice      = auto
        midiconfig      = 
        mpu401          = intelligent
        raw_midi_output = false
        */
        #endregion
        public string MIDI_mididevice { get; set; } = "auto";
        public string MIDI_midiconfig { get; set; } = "";
        public string MIDI_mpu401 { get; set; } = "intelligent";
        public string MIDI_raw_midi_output { get; set; } = "false";

        #region [fluidsynth]
        /*
        [fluidsynth]
        #     soundfont: Path to a SoundFont file in .sf2 format ('default.sf2' by default).
        #                You can use an absolute or relative path, or the name of an .sf2 inside the
        #                'soundfonts' directory within your DOSBox configuration directory.
        #                An optional percentage value after the name will scale the SoundFont's volume.
        #                This is useful for normalising the volume of different SoundFonts.
        #                E.g. 'my_soundfont.sf2 50' will attenuate the volume by 50%.
        #                The percentage value can range from 1 to 800.
        # fsynth_chorus: Chorus effect: 'auto' (default), 'on', 'off', or custom values.
        #                When using custom values:
        #                  All five must be provided in-order and space-separated.
        #                  They are: voice-count level speed depth modulation-wave, where:
        #                    - voice-count is an integer from 0 to 99
        #                    - level is a decimal from 0.0 to 10.0
        #                    - speed is a decimal, measured in Hz, from 0.1 to 5.0
        #                    - depth is a decimal from 0.0 to 21.0
        #                    - modulation-wave is either 'sine' or 'triangle'
        #                  For example: chorus = 3 1.2 0.3 8.0 sine
        #                Note: You can disable the FluidSynth chorus and enable the mixer-level chorus
        #                      on the FluidSynth channel instead, or enable both chorus effects at the
        #                      same time. Whether this sounds good depends on the SoundFont and the
        #                      chorus settings being used.
        # fsynth_reverb: Reverb effect: 'auto' (default), 'on', 'off', or custom values.
        #                When using custom values:
        #                  All four must be provided in-order and space-separated.
        #                  They are: room-size damping width level, where:
        #                    - room-size is a decimal from 0.0 to 1.0
        #                    - damping is a decimal from 0.0 to 1.0
        #                    - width is a decimal from 0.0 to 100.0
        #                    - level is a decimal from 0.0 to 1.0
        #                  For example: reverb = 0.61 0.23 0.76 0.56
        #                Note: You can disable the FluidSynth reverb and enable the mixer-level reverb
        #                      on the FluidSynth channel instead, or enable both reverb effects at the
        #                      same time. Whether this sounds good depends on the SoundFont and the
        #                      reverb settings being used.
        # fsynth_filter: Filter for the FluidSynth audio output:
        #                  off:       Don't filter the output (default).
        #                  <custom>:  Custom filter definition; see 'sb_filter' for details.

        soundfont     = default.sf2
        fsynth_chorus = auto
        fsynth_reverb = auto
        fsynth_filter = off
        */
        #endregion
        public string FLUIDSYNTH_soundfont { get; set; } = "default.sf2";
        public string FLUIDSYNTH_fsynth_chorus { get; set; } = "auto";
        public string FLUIDSYNTH_fsynth_reverb { get; set; } = "auto";
        public string FLUIDSYNTH_fsynth_filter { get; set; } = "off";

        #region [mt32]
        /*
        [mt32]
        #       model: The Roland MT-32/CM-32ML model to use.
        #              You must have the ROM files for the selected model available (see 'romdir').
        #              The lookup order for models that don't specify a version in their name is
        #              performed in order as listed.
        #                auto:       Pick the first available model (default).
        #                cm32l:      Pick the first available CM-32L model.
        #                mt32_old:   Pick the first available "old" MT-32 model (v1.0x).
        #                mt32_new:   Pick the first available "new" MT-32 model (v2.0x).
        #                mt32:       Pick the first available MT-32 model.
        #                <version>:  Use the exact specified model version (e.g., 'mt32_204').
        #              Possible values: auto, cm32l, cm32l_102, cm32l_100, cm32ln_100, mt32, mt32_old, mt32_107, mt32_106, mt32_105, mt32_104, mt32_bluer, mt32_new, mt32_207, mt32_206, mt32_204, mt32_203.
        #      romdir: The directory containing the Roland MT-32/CM-32ML ROMs (unset by default).
        #              The directory can be absolute or relative, or leave it unset to use the
        #              'mt32-roms' directory in your DOSBox configuration directory. Other common
        #              system locations will be checked as well.
        #              Notes:
        #                - The file names of the ROM files do not matter; the ROMS are identified
        #                  by their checksums.
        #                - Both interleaved and non-interlaved ROM files are supported.
        # mt32_filter: Filter for the Roland MT-32/CM-32L audio output:
        #                off:       Don't filter the output (default).
        #                <custom>:  Custom filter definition; see 'sb_filter' for details.

        model       = auto
        romdir      = 
        mt32_filter = off
        */
        #endregion
        public string MT32_model { get; set; } = "auto";
        public string MT32_romdir { get; set; } = "";
        public string MT32_mt32_filter { get; set; } = "off";

        #region [sblaster]
        /*
        [sblaster]
        #              sbtype: Type of Sound Blaster to emulate ('sb16' by default).
        #                      'gb' is Game Blaster.
        #                      Possible values: sb1, sb2, sbpro1, sbpro2, sb16, gb, none.
        #              sbbase: The IO address of the Sound Blaster (220 by default).
        #                      Possible values: 220, 240, 260, 280, 2a0, 2c0, 2e0, 300.
        #                 irq: The IRQ number of the Sound Blaster (7 by default).
        #                      Possible values: 3, 5, 7, 9, 10, 11, 12.
        #                 dma: The DMA channel of the Sound Blaster (1 by default).
        #                      Possible values: 0, 1, 3, 5, 6, 7.
        #                hdma: The High DMA channel of the Sound Blaster 16 (5 by default).
        #                      Possible values: 0, 1, 3, 5, 6, 7.
        #             sbmixer: Allow the Sound Blaster mixer to modify the DOSBox mixer (enabled by default).
        #            sbwarmup: Silence initial DMA audio after card power-on, in milliseconds
        #                      (100 by default). This mitigates pops heard when starting many SB-based games.
        #                      Reduce this if you notice intial playback is missing audio.
        #             oplmode: Type of OPL emulation ('auto' by default).
        #                      On 'auto' the mode is determined by 'sbtype'.
        #                      All OPL modes are AdLib-compatible, except for 'cms'.
        #                      Possible values: auto, cms, opl2, dualopl2, opl3, opl3gold, none.
        #         opl_fadeout: Fade out the OPL synth output after the last IO port write:
        #                        off:       Don't fade out; residual output will play forever (default).
        #                        on:        Wait 0.5s before fading out over a 0.5s period.
        #                        <custom>:  A custom fade-out definition in the following format:
        #                                     WAIT FADE
        #                                   Where WAIT is how long after the last IO port write fading begins,
        #                                   ranging between 100 and 5000 milliseconds; and FADE is the
        #                                   fade-out period, ranging between 10 and 3000 milliseconds.
        #                                   Examples:
        #                                      300 200 (Wait 300ms before fading out over a 200ms period)
        #                                      1000 3000 (Wait 1s before fading out over a 3s period)
        #           sb_filter: Type of filter to emulate for the Sound Blaster digital sound output:
        #                        auto:      Use the appropriate filter determined by 'sbtype'.
        #                        sb1, sb2, sbpro1, sbpro2, sb16:
        #                                   Use the filter of this Sound Blaster model.
        #                        modern:    Use linear interpolation upsampling that acts as a low-pass
        #                                   filter; this is the legacy DOSBox behaviour (default).
        #                        off:       Don't filter the output.
        #                        <custom>:  One or two custom filters in the following format:
        #                                     TYPE ORDER FREQ
        #                                   Where TYPE can be 'hpf' (high-pass) or 'lpf' (low-pass),
        #                                   ORDER is the order of the filter from 1 to 16
        #                                   (1st order = 6dB/oct slope, 2nd order = 12dB/oct, etc.),
        #                                   and FREQ is the cutoff frequency in Hz. Examples:
        #                                      lpf 2 12000
        #                                      hpf 3 120 lfp 1 6500
        # sb_filter_always_on: Force the Sound Blaster filter to be always on
        #                      (disallow programs from turning the filter off; disabled by default).
        #          opl_filter: Type of filter to emulate for the Sound Blaster OPL output:
        #                        auto:      Use the appropriate filter determined by 'sbtype' (default).
        #                        sb1, sb2, sbpro1, sbpro2, sb16:
        #                                   Use the filter of this Sound Blaster model.
        #                        off:       Don't filter the output.
        #                        <custom>:  Custom filter definition; see 'sb_filter' for details.
        #          cms_filter: Filter for the Sound Blaster CMS output:
        #                        on:        Filter the output (default).
        #                        off:       Don't filter the output.
        #                        <custom>:  Custom filter definition; see 'sb_filter' for details.

        sbtype              = sb16
        sbbase              = 220
        irq                 = 7
        dma                 = 1
        hdma                = 5
        sbmixer             = true
        sbwarmup            = 100
        oplmode             = auto
        opl_fadeout         = off
        sb_filter           = modern
        sb_filter_always_on = false
        opl_filter          = auto
        cms_filter          = on
        */
        #endregion
        public string SBLASTER_sbtype { get; set; } = "sb16";
        public string SBLASTER_sbbase { get; set; } = "220";
        public string SBLASTER_irq { get; set; } = "7";
        public string SBLASTER_dma { get; set; } = "1";
        public string SBLASTER_hdma { get; set; } = "5";
        public string SBLASTER_sbmixer { get; set; } = "true";
        public string SBLASTER_sbwarmup { get; set; } = "100";
        public string SBLASTER_oplmode { get; set; } = "auto";
        public string SBLASTER_opl_fadeout { get; set; } = "off";
        public string SBLASTER_sb_filter { get; set; } = "modern";
        public string SBLASTER_sb_filter_always_on { get; set; } = "false";
        public string SBLASTER_opl_filter { get; set; } = "auto";
        public string SBLASTER_cms_filter { get; set; } = "on";

        #region [gus]
        /*
        [gus]
        #        gus: Enable Gravis UltraSound emulation (disabled by default).
        #             The default settings of base address 240, IRQ 5, and DMA 3 have been chosen
        #             so the GUS can coexist with a Sound Blaster card. This works fine for the
        #             majority of programs, but some games and demos expect the GUS factory
        #             defaults of base address 220, IRQ 11, and DMA 1.
        #    gusbase: The IO base address of the Gravis UltraSound (240 by default).
        #             Possible values: 240, 220, 260, 280, 2a0, 2c0, 2e0, 300.
        #     gusirq: The IRQ number of the Gravis UltraSound (5 by default).
        #             Possible values: 3, 5, 7, 9, 10, 11, 12.
        #     gusdma: The DMA channel of the Gravis UltraSound (3 by default).
        #             Possible values: 0, 1, 3, 5, 6, 7.
        #   ultradir: Path to UltraSound directory ('C:\ULTRASND' by default).
        #             In this directory there should be a 'MIDI' directory that contains the patch
        #             files for GUS playback.
        # gus_filter: Filter for the Gravis UltraSound audio output:
        #               off:       Don't filter the output (default).
        #               <custom>:  Custom filter definition; see 'sb_filter' for details.

        gus        = false
        gusbase    = 240
        gusirq     = 5
        gusdma     = 3
        ultradir   = C:\ULTRASND
        gus_filter = off
        */
        #endregion
        public string GUS_gus { get; set; } = "false";
        public string GUS_gusbase { get; set; } = "240";
        public string GUS_gusirq { get; set; } = "5";
        public string GUS_gusdma { get; set; } = "3";
        public string GUS_ultradir { get; set; } = "C:\\ULTRASND";
        public string GUS_gus_filter { get; set; } = "off";

        #region [imfc]
        /*
        [imfc]
        #        imfc: Enable the IBM Music Feature Card (disabled by default).
        #   imfc_base: The IO base address of the IBM Music Feature Card (2A20 by default).
        #              Possible values: 2a20, 2a30.
        #    imfc_irq: The IRQ number of the IBM Music Feature Card (3 by default).
        #              Possible values: 2, 3, 4, 5, 6, 7.
        # imfc_filter: Filter for the IBM Music Feature Card output:
        #                on:        Filter the output (default).
        #                off:       Don't filter the output.
        #                <custom>:  Custom filter definition; see 'sb_filter' for details.

        imfc        = false
        imfc_base   = 2a20
        imfc_irq    = 3
        imfc_filter = on
        */
        #endregion
        public string IMFC_imfc { get; set; } = "false";
        public string IMFC_imfc_base { get; set; } = "2a20";
        public string IMFC_imfc_irq { get; set; } = "3";
        public string IMFC_imfc_filter { get; set; } = "on";

        #region [innovation]
        /*
        [innovation]
        #          sidmodel: Model of chip to emulate in the Innovation SSI-2001 card:
        #                      auto:  Use the 6581 chip.
        #                      6581:  The original chip, known for its bassy and rich character.
        #                      8580:  A later revision that more closely matched the SID specification.
        #                             It fixed the 6581's DC bias and is less prone to distortion.
        #                             The 8580 is an option on reproduction cards, like the DuoSID.
        #                      none:  Disable the card (default).
        #                    Possible values: auto, 6581, 8580, none.
        #          sidclock: The SID chip's clock frequency, which is jumperable on reproduction cards:
        #                      default:  0.895 MHz, per the original SSI-2001 card (default).
        #                      c64ntsc:  1.023 MHz, per NTSC Commodore PCs and the DuoSID.
        #                      c64pal:   0.985 MHz, per PAL Commodore PCs and the DuoSID.
        #                      hardsid:  1.000 MHz, available on the DuoSID.
        #                    Possible values: default, c64ntsc, c64pal, hardsid.
        #           sidport: The IO port address of the Innovation SSI-2001 (280 by default).
        #                    Possible values: 240, 260, 280, 2a0, 2c0.
        #        6581filter: Adjusts the 6581's filtering strength as a percentage from 0 to 100
        #                    (50 by default). The SID's analog filtering meant that each chip was
        #                    physically unique.
        #        8580filter: Adjusts the 8580's filtering strength as a percentage from 0 to 100
        #                    (50 by default).
        # innovation_filter: Filter for the Innovation audio output:
        #                      off:       Don't filter the output (default).
        #                      <custom>:  Custom filter definition; see 'sb_filter' for details.

        sidmodel          = none
        sidclock          = default
        sidport           = 280
        6581filter        = 50
        8580filter        = 50
        innovation_filter = off
        */
        #endregion
        public string INNOVATION_sidmodel { get; set; } = "none";
        public string INNOVATION_sidclock { get; set; } = "default";
        public string INNOVATION_sidport { get; set; } = "280";
        public string INNOVATION_6581filter { get; set; } = "50";
        public string INNOVATION_8580filter { get; set; } = "50";
        public string INNOVATION_innovation_filter { get; set; } = "off";

        #region [speaker]
        /*
        [speaker]
        #           pcspeaker: PC speaker emulation model:
        #                        discrete:  Waveform is created using discrete steps (default).
        #                                   Works well for games that use RealSound-type effects.
        #                        impulse:   Waveform is created using sinc impulses.
        #                                   Recommended for square-wave games, like Commander Keen.
        #                                   While improving accuracy, it is more CPU intensive.
        #                        none/off:  Don't use the PC Speaker.
        #                      Possible values: discrete, impulse, none, off.
        #    pcspeaker_filter: Filter for the PC Speaker output:
        #                        on:        Filter the output (default).
        #                        off:       Don't filter the output.
        #                        <custom>:  Custom filter definition; see 'sb_filter' for details.
        #               tandy: Set the Tandy/PCjr 3 Voice sound emulation:
        #                        auto:  Automatically enable Tandy/PCjr sound for the 'tandy' and 'pcjr'
        #                               machine types only (default).
        #                        on:    Enable Tandy/PCjr sound with DAC support, when possible.
        #                               Most games also need the machine set to 'tandy' or 'pcjr' to work.
        #                        psg:   Only enable the card's three-voice programmable sound generator
        #                               without DAC to avoid conflicts with other cards using DMA 1.
        #                        off:   Disable Tandy/PCjr sound.
        #                      Possible values: auto, on, psg, off.
        #       tandy_fadeout: Fade out the Tandy synth output after the last IO port write:
        #                        off:       Don't fade out; residual output will play forever (default).
        #                        on:        Wait 0.5s before fading out over a 0.5s period.
        #                        <custom>:  Custom fade out definition; see 'opl_fadeout' for details.
        #        tandy_filter: Filter for the Tandy synth output:
        #                        on:        Filter the output (default).
        #                        off:       Don't filter the output.
        #                        <custom>:  Custom filter definition; see 'sb_filter' for details.
        #    tandy_dac_filter: Filter for the Tandy DAC output:
        #                        on:        Filter the output (default).
        #                        off:       Don't filter the output.
        #                        <custom>:  Custom filter definition; see 'sb_filter' for details.
        #             lpt_dac: Type of DAC plugged into the parallel port:
        #                        disney:    Disney Sound Source.
        #                        covox:     Covox Speech Thing.
        #                        ston1:     Stereo-on-1 DAC, in stereo up to 30 kHz.
        #                        none/off:  Don't use a parallel port DAC (default).
        #                      Possible values: none, disney, covox, ston1, off.
        #      lpt_dac_filter: Filter for the LPT DAC audio device(s):
        #                        on:        Filter the output (default).
        #                        off:       Don't filter the output.
        #                        <custom>:  Custom filter definition; see 'sb_filter' for details.
        #            ps1audio: Enable IBM PS/1 Audio emulation (disabled by default).
        #     ps1audio_filter: Filter for the PS/1 Audio synth output:
        #                        on:        Filter the output (default).
        #                        off:       Don't filter the output.
        #                        <custom>:  Custom filter definition; see 'sb_filter' for details.
        # ps1audio_dac_filter: Filter for the PS/1 Audio DAC output:
        #                        on:        Filter the output (default).
        #                        off:       Don't filter the output.
        #                        <custom>:  Custom filter definition; see 'sb_filter' for details.

        pcspeaker           = discrete
        pcspeaker_filter    = on
        tandy               = auto
        tandy_fadeout       = off
        tandy_filter        = on
        tandy_dac_filter    = on
        lpt_dac             = none
        lpt_dac_filter      = on
        ps1audio            = false
        ps1audio_filter     = on
        ps1audio_dac_filter = on
        */
        #endregion
        public string SPEAKER_pcspeaker { get; set; } = "discrete";
        public string SPEAKER_pcspeaker_filter { get; set; } = "on";
        public string SPEAKER_tandy { get; set; } = "auto";
        public string SPEAKER_tandy_fadeout { get; set; } = "off";
        public string SPEAKER_tandy_filter { get; set; } = "on";
        public string SPEAKER_tandy_dac_filter { get; set; } = "on";
        public string SPEAKER_lpt_dac { get; set; } = "none";
        public string SPEAKER_lpt_dac_filter { get; set; } = "on";
        public string SPEAKER_ps1audio { get; set; } = "false";
        public string SPEAKER_ps1audio_filter { get; set; } = "on";
        public string SPEAKER_ps1audio_dac_filter { get; set; } = "on";

        #region [reelmagic]
        /*
        [reelmagic]
        #       reelmagic: ReelMagic (aka REALmagic) MPEG playback support:
        #                    off:       Disable support (default).
        #                    cardonly:  Initialize the card without loading the FMPDRV.EXE driver.
        #                    on:        Initialize the card and load the FMPDRV.EXE on startup.
        #   reelmagic_key: Set the 32-bit magic key used to decode the game's videos:
        #                    auto:      Use the built-in routines to determine the key (default).
        #                    common:    Use the most commonly found key, which is 0x40044041.
        #                    thehorde:  Use The Horde's key, which is 0xC39D7088.
        #                    <custom>:  Set a custom key in hex format (e.g., 0x12345678).
        # reelmagic_fcode: Override the frame rate code used during video playback:
        #                    0:       No override: attempt automatic rate discovery (default).
        #                    1 to 7:  Override the frame rate to one the following (use 1 through 7):
        #                             1=23.976, 2=24, 3=25, 4=29.97, 5=30, 6=50, or 7=59.94 FPS.

        reelmagic       = off
        reelmagic_key   = auto
        reelmagic_fcode = 0
        */
        #endregion
        public string REELMAGIC_reelmagic { get; set; } = "off";
        public string REELMAGIC_reelmagic_key { get; set; } = "auto";
        public string REELMAGIC_reelmagic_fcode { get; set; } = "0";

        #region [joystick]
        /*
        [joystick]
        #                joysticktype: Type of joystick to emulate:
        #                                auto:      Detect and use any joystick(s), if possible (default).
        #                                           Joystick emulation is disabled if no joystick is found.
        #                                2axis:     Support up to two joysticks, each with 2 axis.
        #                                4axis:     Support the first joystick only, as a 4-axis type.
        #                                4axis_2:   Support the second joystick only, as a 4-axis type.
        #                                fcs:       Emulate joystick as an original Thrustmaster FCS.
        #                                ch:        Emulate joystick as an original CH Flightstick.
        #                                hidden:    Prevent DOS from seeing the joystick(s), but enable them
        #                                           for mapping.
        #                                disabled:  Fully disable joysticks: won't be polled, mapped,
        #                                           or visible in DOS.
        #                              Remember to reset DOSBox's mapperfile if you saved it earlier.
        #                              Possible values: auto, 2axis, 4axis, 4axis_2, fcs, ch, hidden, disabled.
        #                       timed: Enable timed intervals for axis (enabled by default).
        #                              Experiment with this option, if your joystick drifts away.
        #                    autofire: Fire continuously as long as the button is pressed
        #                              (disabled by default).
        #                      swap34: Swap the 3rd and the 4th axis (disabled by default).
        #                              Can be useful for certain joysticks.
        #                  buttonwrap: Enable button wrapping at the number of emulated buttons (disabled by default).
        #               circularinput: Enable translation of circular input to square output (disabled by default).
        #                              Try enabling this if your left analog stick can only move in a circle.
        #                    deadzone: Percentage of motion to ignore (10 by default).
        #                              100 turns the stick into a digital one.
        # use_joy_calibration_hotkeys: Enable hotkeys to allow realtime calibration of the joystick's X and Y axes
        #                              (disabled by default). Only consider this if in-game calibration fails and
        #                              other settings have been tried.
        #                                - Ctrl/Cmd+Arrow-keys adjust the axis' scalar value:
        #                                    - Left and Right diminish or magnify the x-axis scalar, respectively.
        #                                    - Down and Up diminish or magnify the y-axis scalar, respectively.
        #                                - Alt+Arrow-keys adjust the axis' offset position:
        #                                    - Left and Right shift X-axis offset in the given direction.
        #                                    - Down and Up shift the Y-axis offset in the given direction.
        #                                - Reset the X and Y calibration using Ctrl+Delete and Ctrl+Home,
        #                                  respectively.
        #                              Each tap will report X or Y calibration values you can set below. When you find
        #                              parameters that work, quit the game, switch this setting back to disabled, and
        #                              populate the reported calibration parameters.
        #           joy_x_calibration: Apply X-axis calibration parameters from the hotkeys ('auto' by default).
        #           joy_y_calibration: Apply Y-axis calibration parameters from the hotkeys ('auto' by default).

        joysticktype                = auto
        timed                       = true
        autofire                    = false
        swap34                      = false
        buttonwrap                  = false
        circularinput               = false
        deadzone                    = 10
        use_joy_calibration_hotkeys = false
        joy_x_calibration           = auto
        joy_y_calibration           = auto
        */
        #endregion
        public string JOYSTICK_joysticktype { get; set; } = "auto";
        public string JOYSTICK_timed { get; set; } = "true";
        public string JOYSTICK_autofire { get; set; } = "false";
        public string JOYSTICK_swap34 { get; set; } = "false";
        public string JOYSTICK_buttonwrap { get; set; } = "false";
        public string JOYSTICK_circularinput { get; set; } = "false";
        public string JOYSTICK_deadzone { get; set; } = "10";
        public string JOYSTICK_use_joy_calibration_hotkeys { get; set; } = "false";
        public string JOYSTICK_joy_x_calibration { get; set; } = "auto";
        public string JOYSTICK_joy_y_calibration { get; set; } = "auto";

        #region [serial]
        /*
        [serial]
        #       serial1: Set type of device connected to the COM1 port.
        #                Can be disabled, dummy, mouse, modem, nullmodem, direct ('dummy' by default).
        #                Additional parameters must be on the same line in the form of
        #                parameter:value. The optional 'irq' parameter is common for all types.
        #                  - for 'mouse':      model (overrides the setting from the [mouse] section)
        #                  - for 'direct':     realport (required), rxdelay (optional).
        #                                      (e.g., realport:COM1, realport:ttyS0).
        #                  - for 'modem':      listenport, sock, bps (all optional).
        #                  - for 'nullmodem':  server, rxdelay, txdelay, telnet, usedtr,
        #                                      transparent, port, inhsocket, sock (all optional).
        #                The 'sock' parameter specifies the protocol to use at both sides of the
        #                connection. Valid values are 0 for TCP, and 1 for ENet reliable UDP.
        #                Example: serial1=modem listenport:5000 sock:1
        #                Possible values: dummy, disabled, mouse, modem, nullmodem, direct.
        #       serial2: See 'serial1' ('dummy' by default).
        #                Possible values: dummy, disabled, mouse, modem, nullmodem, direct.
        #       serial3: See 'serial1' ('disabled' by default).
        #                Possible values: dummy, disabled, mouse, modem, nullmodem, direct.
        #       serial4: See 'serial1' ('disabled' by default).
        #                Possible values: dummy, disabled, mouse, modem, nullmodem, direct.
        # phonebookfile: File used to map fake phone numbers to addresses
        #                ('phonebook.txt' by default).

        serial1       = dummy
        serial2       = dummy
        serial3       = disabled
        serial4       = disabled
        phonebookfile = phonebook.txt
        */
        #endregion
        public string SERIAL_serial1 { get; set; } = "dummy";
        public string SERIAL_serial2 { get; set; } = "dummy";
        public string SERIAL_serial3 { get; set; } = "disabled";
        public string SERIAL_serial4 { get; set; } = "disabled";
        public string SERIAL_phonebookfile { get; set; } = "phonebook.txt";

        #region [dos]
        /*
        [dos]
        #                   xms: Enable XMS support (enabled by default).
        #                   ems: Enable EMS support (enabled by default). Enabled provides the best
        #                        compatibility but certain applications may run better with other choices,
        #                        or require EMS support to be disabled to work at all.
        #                        Possible values: true, emsboard, emm386, false.
        #                   umb: Enable UMB support (enabled by default).
        #                   ver: Set DOS version (5.0 by default). Specify in major.minor format.
        #                        A single number is treated as the major version.
        #                        Common settings are 3.3, 5.0, 6.22, and 7.1.
        #         locale_period: Set locale epoch ('modern' by default). Historic settings (if available
        #                        for the given country) try to mimic old DOS behaviour when displaying
        #                        information such as dates, time, or numbers, modern ones follow current day
        #                        practices for user experience more consistent with typical host systems.
        #                        Possible values: historic, modern.
        #               country: Set DOS country code ('auto' by default).
        #                        This affects country-specific information such as date, time, and decimal
        #                        formats. The list of supported country codes can be displayed using
        #                        '--list-countries' command-line argument. If set to 'auto', the country code
        #                        corresponding to the selected keyboard layout will be used.
        #        keyboardlayout: Keyboard layout code ('auto' by default), i.e. 'us' for US English layout.
        #                        Other possible values are the same as accepted by FreeDOS.
        # expand_shell_variable: Enable expanding environment variables such as %PATH% in the DOS command shell
        #                        (auto by default, enabled if DOS version >= 7.0).
        #                        FreeDOS and MS-DOS 7/8 COMMAND.COM supports this behavior.
        #                        Possible values: auto, true, false.
        #    shell_history_file: File containing persistent command line history ('shell_history.txt'
        #                        by default). Setting it to empty disables persistent shell history.
        #     setver_table_file: File containing the list of applications and assigned DOS versions, in a
        #                        tab-separated format, used by SETVER.EXE as a persistent storage
        #                        (empty by default).
        #    pcjr_memory_config: PCjr memory layout ('expanded' by default).
        #                          expanded:  640 KB total memory with applications residing above 128 KB.
        #                                     Compatible with most games.
        #                          standard:  128 KB total memory with applications residing below 96 KB.
        #                                     Required for some older games (e.g., Jumpman, Troll).
        #                        Possible values: expanded, standard.

        xms                   = true
        ems                   = true
        umb                   = true
        ver                   = 5.0
        locale_period         = modern
        country               = auto
        keyboardlayout        = auto
        expand_shell_variable = auto
        shell_history_file    = shell_history.txt
        setver_table_file     = 
        pcjr_memory_config    = expanded
        */
        #endregion
        public string DOS_xms { get; set; } = "true";
        public string DOS_ems { get; set; } = "true";
        public string DOS_umb { get; set; } = "true";
        public string DOS_ver { get; set; } = "5.0";
        public string DOS_locale_period { get; set; } = "modern";
        public string DOS_country { get; set; } = "auto";
        public string DOS_keyboardlayout { get; set; } = "auto";
        public string DOS_expand_shell_variable { get; set; } = "auto";
        public string DOS_shell_history_file { get; set; } = "shell_history.txt";
        public string DOS_setver_table_file { get; set; } = "";
        public string DOS_pcjr_memory_config { get; set; } = "expanded";

        #region [ipx]
        /*
        [ipx]
        # ipx: Enable IPX over UDP/IP emulation (enabled by default).

        ipx = false
        */
        #endregion
        public string IPX_ipx { get; set; } = "false";

        #region [ethernet]
        /*
        [ethernet]
        #            ne2000: Enable emulation of a Novell NE2000 network card on a software-based
        #                    network (using libslirp) with properties as follows (enabled by default):
        #                      - 255.255.255.0:  Subnet mask of the 10.0.2.0 virtual LAN.
        #                      - 10.0.2.2:       IP of the gateway and DHCP service.
        #                      - 10.0.2.3:       IP of the virtual DNS server.
        #                      - 10.0.2.15:      First IP provided by DHCP, your IP!
        #                    Note: Inside DOS, setting this up requires an NE2000 packet driver, DHCP
        #                          client, and TCP/IP stack. You might need port-forwarding from the host
        #                          into the DOS guest, and from your router to your host when acting as the
        #                          server for multiplayer games.
        #           nicbase: Base address of the NE2000 card (300 by default).
        #                    Note: Addresses 220 and 240 might not be available as they're assigned to the
        #                          Sound Blaster and Gravis UltraSound by default.
        #                    Possible values: 200, 220, 240, 260, 280, 2c0, 300, 320, 340, 360.
        #            nicirq: The interrupt used by the NE2000 card (3 by default).
        #                    Note: IRQs 3 and 5 might not be available as they're assigned to
        #                          'serial2' and the Gravis UltraSound by default.
        #                    Possible values: 3, 4, 5, 9, 10, 11, 12, 14, 15.
        #           macaddr: The MAC address of the NE2000 card ('AC:DE:48:88:99:AA' by default).
        # tcp_port_forwards: Forward one or more TCP ports from the host into the DOS guest
        #                    (unset by default).
        #                    The format is:
        #                      port1  port2  port3 ... (e.g., 21 80 443)
        #                      This will forward FTP, HTTP, and HTTPS into the DOS guest.
        #                    If the ports are privileged on the host, a mapping can be used
        #                      host:guest  ..., (e.g., 8021:21 8080:80)
        #                      This will forward ports 8021 and 8080 to FTP and HTTP in the guest.
        #                    A range of adjacent ports can be abbreviated with a dash:
        #                      start-end ... (e.g., 27910-27960)
        #                      This will forward ports 27910 to 27960 into the DOS guest.
        #                    Mappings and ranges can be combined, too:
        #                      hstart-hend:gstart-gend ..., (e.g, 8040-8080:20-60)
        #                      This forwards ports 8040 to 8080 into 20 to 60 in the guest.
        #                    Notes:
        #                      - If mapped ranges differ, the shorter range is extended to fit.
        #                      - If conflicting host ports are given, only the first one is setup.
        #                      - If conflicting guest ports are given, the latter rule takes precedent.
        # udp_port_forwards: Forward one or more UDP ports from the host into the DOS guest
        #                    (unset by default). The format is the same as for TCP port forwards.

        ne2000            = true
        nicbase           = 300
        nicirq            = 3
        macaddr           = AC:DE:48:88:99:AA
        tcp_port_forwards = 
        udp_port_forwards = 
        */
        #endregion
        public string ETHERNET_ne2000 { get; set; } = "true";
        public string ETHERNET_nicbase { get; set; } = "300";
        public string ETHERNET_nicirq { get; set; } = "3";
        public string ETHERNET_macaddr { get; set; } = "AC:DE:48:88:99:AA";
        public string ETHERNET_tcp_port_forwards { get; set; } = "";
        public string ETHERNET_udp_port_forwards { get; set; } = "";

        #region [autoexec]
        /*
        [autoexec]
        # Lines in this section will be run at startup.
        # You can put your MOUNT lines here.
        */
        #endregion
        // It is neccecary to handle this section manually
        //public string AUTOEXEC_autoexec { get; set; } = "";
    }
}
