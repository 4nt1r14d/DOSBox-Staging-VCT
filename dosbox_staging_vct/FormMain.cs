using IniParser.Model;
using IniParser;
using System.IO;
using System.Drawing.Text;
using System.Resources;
using System.Reflection;
using System.Globalization;
using dosbox_staging_vct.Resources;
using System.Reflection.Metadata;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using dosbox_staging_vct.Properties;


namespace dosbox_staging_vct
{
    public partial class FormMain : Form
    {
        ResourceManager resourceManager;

        public FormMain()
        {
            InitializeComponent();
            resourceManager = new ResourceManager(typeof(FormMain));
        }

        private List<string>? listBoxItems; // To store the items in the listbox

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                // Set the language
                SetLanguage();

                // Initialize the filter TextBox
                InitializeFilterTextBox();

                // Set the ToolStripLabel text
                SetToolStripLabelText();

                // Set the help tooltips
                // I have created a help form instead of using tooltips
                //SetToolTips();

                // Set the ComboBox options from a JSON file
                SetComboBoxOptions(Properties.Settings.Default.JsonComboBoxOptionsFileName);

                // Load the User confs in the listbox present in the user conf folder
                LoadAllUserConfs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ListBoxUserConfs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? userConfFileName = ListBoxUserConfs.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(userConfFileName))
            {
                return;
            }

            try
            {
                if (PathsAreSet())
                {
                    // Set the ToolStripLabel text
                    SetToolStripLabelText();

                    // Load the user conf
                    LoadUserConf(userConfFileName);
                    LoadAutoexecSection(userConfFileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TextBoxFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = TextBoxFilter.Text.ToLower();

                // Verify if the text in the TextBox is 'Filter'
                if (searchText == "filter")
                {
                    // Si es igual, considera que no hay filtro aplicado
                    searchText = string.Empty;
                }

                ListBoxUserConfs.Items.Clear();

                if (string.IsNullOrEmpty(searchText))
                {
                    if (listBoxItems != null)
                    {
                        ListBoxUserConfs.Items.AddRange(listBoxItems.ToArray());
                    }

                }
                else
                {
                    if (listBoxItems != null)
                    {
                        // Filter the items in the listbox (case insensitive)
                        var filteredItems = listBoxItems.Where(item => item.ToLower().Contains(searchText));
                        ListBoxUserConfs.Items.AddRange(filteredItems.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TextBoxFilter_Enter(object sender, EventArgs e)
        {
            // Clean the TextBox if the text is 'Filter'
            if (TextBoxFilter.Text == "Filter")
            {
                TextBoxFilter.Text = "";
                TextBoxFilter.ForeColor = Color.Black;
                TextBoxFilter.Font = new Font(TextBoxFilter.Font, FontStyle.Regular);
            }
        }

        private void TextBoxFilter_Leave(object sender, EventArgs e)
        {
            // If the TextBox is empty, set the text to 'Filter', the color to gray and the font to cursive
            if (string.IsNullOrWhiteSpace(TextBoxFilter.Text))
            {
                TextBoxFilter.Text = "Filter";
                //TextBoxFilter.ForeColor = Color.Gray;
                TextBoxFilter.ForeColor = GlobalSettings.ColorHighlight;
                TextBoxFilter.Font = new Font(TextBoxFilter.Font, FontStyle.Bold | FontStyle.Italic);
            }
        }

        private void InitializeFilterTextBox()
        {
            // Set the text in the TextBox to 'Filter', the color to gray and the font to bold and italic
            TextBoxFilter.Text = "Filter";
            //TextBoxFilter.ForeColor = Color.Gray;
            TextBoxFilter.ForeColor = GlobalSettings.ColorHighlight;
            TextBoxFilter.Font = new Font(TextBoxFilter.Font, FontStyle.Bold | FontStyle.Italic);
        }

        private void SetToolStripLabelText()
        {
            // If no config is selected in the listbox, set the text to 'No config selected'
            if (ListBoxUserConfs.SelectedItem == null)
            {
                ToolStripLabel.Text = "No config selected";
            }
            else
            {
                ToolStripLabel.Text = "Editing: " + ListBoxUserConfs.SelectedItem.ToString();
            }
        }

        private static bool PathsAreSet()
        {
            bool pathsAreSet = false;

            if (!File.Exists(Properties.Settings.Default.DosBoxStagingExeFilePath))
            {
                throw new Exception("The path to the DOSBox Staging executable is invalid. Please set the correct path in the options.");
            }
            else if (!File.Exists(Properties.Settings.Default.GlobalConfFilePath))
            {
                throw new Exception("The path to the global conf file is invalid. Please set the correct path in the options.");
            }
            else if (!Directory.Exists(Properties.Settings.Default.UserConfFolderPath))
            {
                throw new Exception("The path to the user conf folder is invalid. Please set the correct path in the options.");
            }
            else
            {
                pathsAreSet = true;
            }

            return pathsAreSet;
        }

        public void LoadUserConf(string? userConfFileName)
        {
            // If paths are not set, return
            if (!PathsAreSet())
            {
                return;
            }

            // Check if the selected item in the listbox is not null
            if (userConfFileName == null)
            {
                return;
            }

            string userConfFilePath = Path.Combine(Properties.Settings.Default.UserConfFolderPath, userConfFileName);

            // If the user con file exists
            if (!File.Exists(userConfFilePath))
            {
                return;
            }

            // Load the user conf (and the global conf to see what user properties are different from the global properties)
            string globalConfFilePath = Properties.Settings.Default.GlobalConfFilePath;
            UserConf userConf = GetUserConf(userConfFilePath);
            GlobalConf globalConf = GetGlobalConf(globalConfFilePath);

            // Fill the controls with the user properties (or the global properties if are the same as user properties)
            LoadConf(globalConf, userConf);
        }

        private static GlobalConf GetGlobalConf(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            var parser = new FileIniDataParser();
            parser.Parser.Configuration.CommentString = "#";
            parser.Parser.Configuration.SkipInvalidLines = true; // To process the autoexec section correctly

            IniData iniData = parser.ReadFile(filePath);

            // Recuperación de los datos
            GlobalConf globalConf = new()
            {
                // [sdl]
                SDL_fullscreen = iniData["sdl"]["fullscreen"],
                SDL_display = iniData["sdl"]["display"],
                SDL_fullresolution = iniData["sdl"]["fullresolution"],
                SDL_windowresolution = iniData["sdl"]["windowresolution"],
                SDL_window_position = iniData["sdl"]["window_position"],
                SDL_window_decorations = iniData["sdl"]["window_decorations"],
                SDL_transparency = iniData["sdl"]["transparency"],
                SDL_host_rate = iniData["sdl"]["host_rate"],
                SDL_vsync = iniData["sdl"]["vsync"],
                SDL_vsync_skip = iniData["sdl"]["vsync_skip"],
                SDL_presentation_mode = iniData["sdl"]["presentation_mode"],
                SDL_output = iniData["sdl"]["output"],
                SDL_texture_renderer = iniData["sdl"]["texture_renderer"],
                SDL_waitonerror = iniData["sdl"]["waitonerror"],
                SDL_priority = iniData["sdl"]["priority"],
                SDL_mute_when_inactive = iniData["sdl"]["mute_when_inactive"],
                SDL_pause_when_inactive = iniData["sdl"]["pause_when_inactive"],
                SDL_mapperfile = iniData["sdl"]["mapperfile"],
                SDL_screensaver = iniData["sdl"]["screensaver"],

                // [dosbox]
                DOSBOX_language = iniData["dosbox"]["language"],
                DOSBOX_machine = iniData["dosbox"]["machine"],
                DOSBOX_memsize = iniData["dosbox"]["memsize"],
                DOSBOX_mcb_fault_strategy = iniData["dosbox"]["mcb_fault_strategy"],
                DOSBOX_vmemsize = iniData["dosbox"]["vmemsize"],
                DOSBOX_vmem_delay = iniData["dosbox"]["vmem_delay"],
                DOSBOX_dos_rate = iniData["dosbox"]["dos_rate"],
                DOSBOX_vesa_modes = iniData["dosbox"]["vesa_modes"],
                DOSBOX_vga_8dot_font = iniData["dosbox"]["vga_8dot_font"],
                DOSBOX_vga_render_per_scanline = iniData["dosbox"]["vga_render_per_scanline"],
                DOSBOX_speed_mods = iniData["dosbox"]["speed_mods"],
                DOSBOX_autoexec_section = iniData["dosbox"]["autoexec_section"],
                DOSBOX_automount = iniData["dosbox"]["automount"],
                DOSBOX_startup_verbosity = iniData["dosbox"]["startup_verbosity"],
                DOSBOX_allow_write_protected_files = iniData["dosbox"]["allow_write_protected_files"],
                DOSBOX_shell_config_shortcuts = iniData["dosbox"]["shell_config_shortcuts"],

                // [render]
                RENDER_aspect = iniData["render"]["aspect"],
                RENDER_integer_scaling = iniData["render"]["integer_scaling"],
                RENDER_viewport = iniData["render"]["viewport"],
                RENDER_monochrome_palette = iniData["render"]["monochrome_palette"],
                RENDER_cga_colors = iniData["render"]["cga_colors"],
                RENDER_glshader = iniData["render"]["glshader"],

                // [composite]
                COMPOSITE_composite = iniData["composite"]["composite"],
                COMPOSITE_era = iniData["composite"]["era"],
                COMPOSITE_hue = iniData["composite"]["hue"],
                COMPOSITE_saturation = iniData["composite"]["saturation"],
                COMPOSITE_contrast = iniData["composite"]["contrast"],
                COMPOSITE_brightness = iniData["composite"]["brightness"],
                COMPOSITE_convergence = iniData["composite"]["convergence"],

                // [cpu]
                CPU_core = iniData["cpu"]["core"],
                CPU_cputype = iniData["cpu"]["cputype"],
                CPU_cycles = iniData["cpu"]["cycles"],
                CPU_cycleup = iniData["cpu"]["cycleup"],
                CPU_cycledown = iniData["cpu"]["cycledown"],

                // [voodoo]
                VOODOO_voodoo = iniData["voodoo"]["voodoo"],
                VOODOO_voodoo_memsize = iniData["voodoo"]["voodoo_memsize"],
                VOODOO_voodoo_multithreading = iniData["voodoo"]["voodoo_multithreading"],
                VOODOO_voodoo_bilinear_filtering = iniData["voodoo"]["voodoo_bilinear_filtering"],

                // [capture]
                CAPTURE_capture_dir = iniData["capture"]["capture_dir"],
                CAPTURE_default_image_capture_formats = iniData["capture"]["default_image_capture_formats"],

                // [mouse]
                MOUSE_mouse_capture = iniData["mouse"]["mouse_capture"],
                MOUSE_mouse_middle_release = iniData["mouse"]["mouse_middle_release"],
                MOUSE_mouse_multi_display_aware = iniData["mouse"]["mouse_multi_display_aware"],
                MOUSE_mouse_sensitivity = iniData["mouse"]["mouse_sensitivity"],
                MOUSE_mouse_raw_input = iniData["mouse"]["mouse_raw_input"],
                MOUSE_dos_mouse_driver = iniData["mouse"]["dos_mouse_driver"],
                MOUSE_dos_mouse_immediate = iniData["mouse"]["dos_mouse_immediate"],
                MOUSE_ps2_mouse_model = iniData["mouse"]["ps2_mouse_model"],
                MOUSE_com_mouse_model = iniData["mouse"]["com_mouse_model"],
                MOUSE_vmware_mouse = iniData["mouse"]["vmware_mouse"],
                MOUSE_virtualbox_mouse = iniData["mouse"]["virtualbox_mouse"],

                // [mixer]
                MIXER_nosound = iniData["mixer"]["nosound"],
                MIXER_rate = iniData["mixer"]["rate"],
                MIXER_blocksize = iniData["mixer"]["blocksize"],
                MIXER_prebuffer = iniData["mixer"]["prebuffer"],
                MIXER_negotiate = iniData["mixer"]["negotiate"],
                MIXER_compressor = iniData["mixer"]["compressor"],
                MIXER_crossfeed = iniData["mixer"]["crossfeed"],
                MIXER_reverb = iniData["mixer"]["reverb"],
                MIXER_chorus = iniData["mixer"]["chorus"],

                // [midi]
                MIDI_mididevice = iniData["midi"]["mididevice"],
                MIDI_midiconfig = iniData["midi"]["midiconfig"],
                MIDI_mpu401 = iniData["midi"]["mpu401"],
                MIDI_raw_midi_output = iniData["midi"]["raw_midi_output"],

                // [fluidsynth]
                FLUIDSYNTH_soundfont = iniData["fluidsynth"]["soundfont"],
                FLUIDSYNTH_fsynth_chorus = iniData["fluidsynth"]["fsynth_chorus"],
                FLUIDSYNTH_fsynth_reverb = iniData["fluidsynth"]["fsynth_reverb"],
                FLUIDSYNTH_fsynth_filter = iniData["fluidsynth"]["fsynth_filter"],

                // [mt32]
                MT32_model = iniData["mt32"]["model"],
                MT32_romdir = iniData["mt32"]["romdir"],
                MT32_mt32_filter = iniData["mt32"]["mt32_filter"],

                // [sblaster]
                SBLASTER_sbtype = iniData["sblaster"]["sbtype"],
                SBLASTER_sbbase = iniData["sblaster"]["sbbase"],
                SBLASTER_irq = iniData["sblaster"]["irq"],
                SBLASTER_dma = iniData["sblaster"]["dma"],
                SBLASTER_hdma = iniData["sblaster"]["hdma"],
                SBLASTER_sbmixer = iniData["sblaster"]["sbmixer"],
                SBLASTER_sbwarmup = iniData["sblaster"]["sbwarmup"],
                SBLASTER_oplmode = iniData["sblaster"]["oplmode"],
                SBLASTER_opl_fadeout = iniData["sblaster"]["opl_fadeout"],
                SBLASTER_sb_filter = iniData["sblaster"]["sb_filter"],
                SBLASTER_sb_filter_always_on = iniData["sblaster"]["sb_filter_always_on"],
                SBLASTER_opl_filter = iniData["sblaster"]["opl_filter"],
                SBLASTER_cms_filter = iniData["sblaster"]["cms_filter"],

                // [gus]
                GUS_gus = iniData["gus"]["gus"],
                GUS_gusbase = iniData["gus"]["gusbase"],
                GUS_gusirq = iniData["gus"]["gusirq"],
                GUS_gusdma = iniData["gus"]["gusdma"],
                GUS_ultradir = iniData["gus"]["ultradir"],
                GUS_gus_filter = iniData["gus"]["gus_filter"],

                // [imfc]
                IMFC_imfc = iniData["imfc"]["imfc"],
                IMFC_imfc_base = iniData["imfc"]["imfc_base"],
                IMFC_imfc_irq = iniData["imfc"]["imfc_irq"],
                IMFC_imfc_filter = iniData["imfc"]["imfc_filter"],

                // [innovation]
                INNOVATION_sidmodel = iniData["innovation"]["sidmodel"],
                INNOVATION_sidclock = iniData["innovation"]["sidclock"],
                INNOVATION_sidport = iniData["innovation"]["sidport"],
                INNOVATION_6581filter = iniData["innovation"]["6581filter"],
                INNOVATION_8580filter = iniData["innovation"]["8580filter"],
                INNOVATION_innovation_filter = iniData["innovation"]["innovation_filter"],

                // [speaker]
                SPEAKER_pcspeaker = iniData["speaker"]["pcspeaker"],
                SPEAKER_pcspeaker_filter = iniData["speaker"]["pcspeaker_filter"],
                SPEAKER_tandy = iniData["speaker"]["tandy"],
                SPEAKER_tandy_fadeout = iniData["speaker"]["tandy_fadeout"],
                SPEAKER_tandy_filter = iniData["speaker"]["tandy_filter"],
                SPEAKER_tandy_dac_filter = iniData["speaker"]["tandy_dac_filter"],
                SPEAKER_lpt_dac = iniData["speaker"]["lpt_dac"],
                SPEAKER_lpt_dac_filter = iniData["speaker"]["lpt_dac_filter"],
                SPEAKER_ps1audio = iniData["speaker"]["ps1audio"],
                SPEAKER_ps1audio_filter = iniData["speaker"]["ps1audio_filter"],
                SPEAKER_ps1audio_dac_filter = iniData["speaker"]["ps1audio_dac_filter"],

                // [reelmagic]
                REELMAGIC_reelmagic = iniData["reelmagic"]["reelmagic"],
                REELMAGIC_reelmagic_key = iniData["reelmagic"]["reelmagic_key"],
                REELMAGIC_reelmagic_fcode = iniData["reelmagic"]["reelmagic_fcode"],

                // [joystick]
                JOYSTICK_joysticktype = iniData["joystick"]["joysticktype"],
                JOYSTICK_timed = iniData["joystick"]["timed"],
                JOYSTICK_autofire = iniData["joystick"]["autofire"],
                JOYSTICK_swap34 = iniData["joystick"]["swap34"],
                JOYSTICK_buttonwrap = iniData["joystick"]["buttonwrap"],
                JOYSTICK_circularinput = iniData["joystick"]["circularinput"],
                JOYSTICK_deadzone = iniData["joystick"]["deadzone"],
                JOYSTICK_use_joy_calibration_hotkeys = iniData["joystick"]["use_joy_calibration_hotkeys"],
                JOYSTICK_joy_x_calibration = iniData["joystick"]["joy_x_calibration"],
                JOYSTICK_joy_y_calibration = iniData["joystick"]["joy_y_calibration"],

                // [serial]
                SERIAL_serial1 = iniData["serial"]["serial1"],
                SERIAL_serial2 = iniData["serial"]["serial2"],
                SERIAL_serial3 = iniData["serial"]["serial3"],
                SERIAL_serial4 = iniData["serial"]["serial4"],
                SERIAL_phonebookfile = iniData["serial"]["phonebookfile"],

                // [dos]
                DOS_xms = iniData["dos"]["xms"],
                DOS_ems = iniData["dos"]["ems"],
                DOS_umb = iniData["dos"]["umb"],
                DOS_ver = iniData["dos"]["ver"],
                DOS_locale_period = iniData["dos"]["locale_period"],
                DOS_country = iniData["dos"]["country"],
                DOS_keyboardlayout = iniData["dos"]["keyboardlayout"],
                DOS_expand_shell_variable = iniData["dos"]["expand_shell_variable"],
                DOS_shell_history_file = iniData["dos"]["shell_history_file"],
                DOS_setver_table_file = iniData["dos"]["setver_table_file"],
                DOS_pcjr_memory_config = iniData["dos"]["pcjr_memory_config"],

                // [ipx]
                IPX_ipx = iniData["ipx"]["ipx"],

                // [ethernet]
                ETHERNET_ne2000 = iniData["ethernet"]["ne2000"],
                ETHERNET_nicbase = iniData["ethernet"]["nicbase"],
                ETHERNET_nicirq = iniData["ethernet"]["nicirq"],
                ETHERNET_macaddr = iniData["ethernet"]["macaddr"],
                ETHERNET_tcp_port_forwards = iniData["ethernet"]["tcp_port_forwards"],
                ETHERNET_udp_port_forwards = iniData["ethernet"]["udp_port_forwards"],

                // [autoexec]
                // It is neccecary to handle this section manually
                //AUTOEXEC_autoexec = iniData["autoexec"]["autoexec"]
            };
            return globalConf;
        }

        private static UserConf GetUserConf(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            var parser = new FileIniDataParser();
            parser.Parser.Configuration.CommentString = "#";
            parser.Parser.Configuration.SkipInvalidLines = true; // To process the autoexec section correctly

            IniData iniData = parser.ReadFile(filePath);

            // Recuperación de los datos
            UserConf userConf = new()
            {
                // [sdl]
                SDL_fullscreen = iniData["sdl"]["fullscreen"],
                SDL_display = iniData["sdl"]["display"],
                SDL_fullresolution = iniData["sdl"]["fullresolution"],
                SDL_windowresolution = iniData["sdl"]["windowresolution"],
                SDL_window_position = iniData["sdl"]["window_position"],
                SDL_window_decorations = iniData["sdl"]["window_decorations"],
                SDL_transparency = iniData["sdl"]["transparency"],
                SDL_host_rate = iniData["sdl"]["host_rate"],
                SDL_vsync = iniData["sdl"]["vsync"],
                SDL_vsync_skip = iniData["sdl"]["vsync_skip"],
                SDL_presentation_mode = iniData["sdl"]["presentation_mode"],
                SDL_output = iniData["sdl"]["output"],
                SDL_texture_renderer = iniData["sdl"]["texture_renderer"],
                SDL_waitonerror = iniData["sdl"]["waitonerror"],
                SDL_priority = iniData["sdl"]["priority"],
                SDL_mute_when_inactive = iniData["sdl"]["mute_when_inactive"],
                SDL_pause_when_inactive = iniData["sdl"]["pause_when_inactive"],
                SDL_mapperfile = iniData["sdl"]["mapperfile"],
                SDL_screensaver = iniData["sdl"]["screensaver"],

                // [dosbox]
                DOSBOX_language = iniData["dosbox"]["language"],
                DOSBOX_machine = iniData["dosbox"]["machine"],
                DOSBOX_memsize = iniData["dosbox"]["memsize"],
                DOSBOX_mcb_fault_strategy = iniData["dosbox"]["mcb_fault_strategy"],
                DOSBOX_vmemsize = iniData["dosbox"]["vmemsize"],
                DOSBOX_vmem_delay = iniData["dosbox"]["vmem_delay"],
                DOSBOX_dos_rate = iniData["dosbox"]["dos_rate"],
                DOSBOX_vesa_modes = iniData["dosbox"]["vesa_modes"],
                DOSBOX_vga_8dot_font = iniData["dosbox"]["vga_8dot_font"],
                DOSBOX_vga_render_per_scanline = iniData["dosbox"]["vga_render_per_scanline"],
                DOSBOX_speed_mods = iniData["dosbox"]["speed_mods"],
                DOSBOX_autoexec_section = iniData["dosbox"]["autoexec_section"],
                DOSBOX_automount = iniData["dosbox"]["automount"],
                DOSBOX_startup_verbosity = iniData["dosbox"]["startup_verbosity"],
                DOSBOX_allow_write_protected_files = iniData["dosbox"]["allow_write_protected_files"],
                DOSBOX_shell_config_shortcuts = iniData["dosbox"]["shell_config_shortcuts"],

                // [render]
                RENDER_aspect = iniData["render"]["aspect"],
                RENDER_integer_scaling = iniData["render"]["integer_scaling"],
                RENDER_viewport = iniData["render"]["viewport"],
                RENDER_monochrome_palette = iniData["render"]["monochrome_palette"],
                RENDER_cga_colors = iniData["render"]["cga_colors"],
                RENDER_glshader = iniData["render"]["glshader"],

                // [composite]
                COMPOSITE_composite = iniData["composite"]["composite"],
                COMPOSITE_era = iniData["composite"]["era"],
                COMPOSITE_hue = iniData["composite"]["hue"],
                COMPOSITE_saturation = iniData["composite"]["saturation"],
                COMPOSITE_contrast = iniData["composite"]["contrast"],
                COMPOSITE_brightness = iniData["composite"]["brightness"],
                COMPOSITE_convergence = iniData["composite"]["convergence"],

                // [cpu]
                CPU_core = iniData["cpu"]["core"],
                CPU_cputype = iniData["cpu"]["cputype"],
                CPU_cycles = iniData["cpu"]["cycles"],
                CPU_cycleup = iniData["cpu"]["cycleup"],
                CPU_cycledown = iniData["cpu"]["cycledown"],

                // [voodoo]
                VOODOO_voodoo = iniData["voodoo"]["voodoo"],
                VOODOO_voodoo_memsize = iniData["voodoo"]["voodoo_memsize"],
                VOODOO_voodoo_multithreading = iniData["voodoo"]["voodoo_multithreading"],
                VOODOO_voodoo_bilinear_filtering = iniData["voodoo"]["voodoo_bilinear_filtering"],

                // [capture]
                CAPTURE_capture_dir = iniData["capture"]["capture_dir"],
                CAPTURE_default_image_capture_formats = iniData["capture"]["default_image_capture_formats"],

                // [mouse]
                MOUSE_mouse_capture = iniData["mouse"]["mouse_capture"],
                MOUSE_mouse_middle_release = iniData["mouse"]["mouse_middle_release"],
                MOUSE_mouse_multi_display_aware = iniData["mouse"]["mouse_multi_display_aware"],
                MOUSE_mouse_sensitivity = iniData["mouse"]["mouse_sensitivity"],
                MOUSE_mouse_raw_input = iniData["mouse"]["mouse_raw_input"],
                MOUSE_dos_mouse_driver = iniData["mouse"]["dos_mouse_driver"],
                MOUSE_dos_mouse_immediate = iniData["mouse"]["dos_mouse_immediate"],
                MOUSE_ps2_mouse_model = iniData["mouse"]["ps2_mouse_model"],
                MOUSE_com_mouse_model = iniData["mouse"]["com_mouse_model"],
                MOUSE_vmware_mouse = iniData["mouse"]["vmware_mouse"],
                MOUSE_virtualbox_mouse = iniData["mouse"]["virtualbox_mouse"],

                // [mixer]
                MIXER_nosound = iniData["mixer"]["nosound"],
                MIXER_rate = iniData["mixer"]["rate"],
                MIXER_blocksize = iniData["mixer"]["blocksize"],
                MIXER_prebuffer = iniData["mixer"]["prebuffer"],
                MIXER_negotiate = iniData["mixer"]["negotiate"],
                MIXER_compressor = iniData["mixer"]["compressor"],
                MIXER_crossfeed = iniData["mixer"]["crossfeed"],
                MIXER_reverb = iniData["mixer"]["reverb"],
                MIXER_chorus = iniData["mixer"]["chorus"],

                // [midi]
                MIDI_mididevice = iniData["midi"]["mididevice"],
                MIDI_midiconfig = iniData["midi"]["midiconfig"],
                MIDI_mpu401 = iniData["midi"]["mpu401"],
                MIDI_raw_midi_output = iniData["midi"]["raw_midi_output"],

                // [fluidsynth]
                FLUIDSYNTH_soundfont = iniData["fluidsynth"]["soundfont"],
                FLUIDSYNTH_fsynth_chorus = iniData["fluidsynth"]["fsynth_chorus"],
                FLUIDSYNTH_fsynth_reverb = iniData["fluidsynth"]["fsynth_reverb"],
                FLUIDSYNTH_fsynth_filter = iniData["fluidsynth"]["fsynth_filter"],

                // [mt32]
                MT32_model = iniData["mt32"]["model"],
                MT32_romdir = iniData["mt32"]["romdir"],
                MT32_mt32_filter = iniData["mt32"]["mt32_filter"],

                // [sblaster]
                SBLASTER_sbtype = iniData["sblaster"]["sbtype"],
                SBLASTER_sbbase = iniData["sblaster"]["sbbase"],
                SBLASTER_irq = iniData["sblaster"]["irq"],
                SBLASTER_dma = iniData["sblaster"]["dma"],
                SBLASTER_hdma = iniData["sblaster"]["hdma"],
                SBLASTER_sbmixer = iniData["sblaster"]["sbmixer"],
                SBLASTER_sbwarmup = iniData["sblaster"]["sbwarmup"],
                SBLASTER_oplmode = iniData["sblaster"]["oplmode"],
                SBLASTER_opl_fadeout = iniData["sblaster"]["opl_fadeout"],
                SBLASTER_sb_filter = iniData["sblaster"]["sb_filter"],
                SBLASTER_sb_filter_always_on = iniData["sblaster"]["sb_filter_always_on"],
                SBLASTER_opl_filter = iniData["sblaster"]["opl_filter"],
                SBLASTER_cms_filter = iniData["sblaster"]["cms_filter"],

                // [gus]
                GUS_gus = iniData["gus"]["gus"],
                GUS_gusbase = iniData["gus"]["gusbase"],
                GUS_gusirq = iniData["gus"]["gusirq"],
                GUS_gusdma = iniData["gus"]["gusdma"],
                GUS_ultradir = iniData["gus"]["ultradir"],
                GUS_gus_filter = iniData["gus"]["gus_filter"],

                // [imfc]
                IMFC_imfc = iniData["imfc"]["imfc"],
                IMFC_imfc_base = iniData["imfc"]["imfc_base"],
                IMFC_imfc_irq = iniData["imfc"]["imfc_irq"],
                IMFC_imfc_filter = iniData["imfc"]["imfc_filter"],

                // [innovation]
                INNOVATION_sidmodel = iniData["innovation"]["sidmodel"],
                INNOVATION_sidclock = iniData["innovation"]["sidclock"],
                INNOVATION_sidport = iniData["innovation"]["sidport"],
                INNOVATION_6581filter = iniData["innovation"]["6581filter"],
                INNOVATION_8580filter = iniData["innovation"]["8580filter"],
                INNOVATION_innovation_filter = iniData["innovation"]["innovation_filter"],

                // [speaker]
                SPEAKER_pcspeaker = iniData["speaker"]["pcspeaker"],
                SPEAKER_pcspeaker_filter = iniData["speaker"]["pcspeaker_filter"],
                SPEAKER_tandy = iniData["speaker"]["tandy"],
                SPEAKER_tandy_fadeout = iniData["speaker"]["tandy_fadeout"],
                SPEAKER_tandy_filter = iniData["speaker"]["tandy_filter"],
                SPEAKER_tandy_dac_filter = iniData["speaker"]["tandy_dac_filter"],
                SPEAKER_lpt_dac = iniData["speaker"]["lpt_dac"],
                SPEAKER_lpt_dac_filter = iniData["speaker"]["lpt_dac_filter"],
                SPEAKER_ps1audio = iniData["speaker"]["ps1audio"],
                SPEAKER_ps1audio_filter = iniData["speaker"]["ps1audio_filter"],
                SPEAKER_ps1audio_dac_filter = iniData["speaker"]["ps1audio_dac_filter"],

                // [reelmagic]
                REELMAGIC_reelmagic = iniData["reelmagic"]["reelmagic"],
                REELMAGIC_reelmagic_key = iniData["reelmagic"]["reelmagic_key"],
                REELMAGIC_reelmagic_fcode = iniData["reelmagic"]["reelmagic_fcode"],

                // [joystick]
                JOYSTICK_joysticktype = iniData["joystick"]["joysticktype"],
                JOYSTICK_timed = iniData["joystick"]["timed"],
                JOYSTICK_autofire = iniData["joystick"]["autofire"],
                JOYSTICK_swap34 = iniData["joystick"]["swap34"],
                JOYSTICK_buttonwrap = iniData["joystick"]["buttonwrap"],
                JOYSTICK_circularinput = iniData["joystick"]["circularinput"],
                JOYSTICK_deadzone = iniData["joystick"]["deadzone"],
                JOYSTICK_use_joy_calibration_hotkeys = iniData["joystick"]["use_joy_calibration_hotkeys"],
                JOYSTICK_joy_x_calibration = iniData["joystick"]["joy_x_calibration"],
                JOYSTICK_joy_y_calibration = iniData["joystick"]["joy_y_calibration"],

                // [serial]
                SERIAL_serial1 = iniData["serial"]["serial1"],
                SERIAL_serial2 = iniData["serial"]["serial2"],
                SERIAL_serial3 = iniData["serial"]["serial3"],
                SERIAL_serial4 = iniData["serial"]["serial4"],
                SERIAL_phonebookfile = iniData["serial"]["phonebookfile"],

                // [dos]
                DOS_xms = iniData["dos"]["xms"],
                DOS_ems = iniData["dos"]["ems"],
                DOS_umb = iniData["dos"]["umb"],
                DOS_ver = iniData["dos"]["ver"],
                DOS_locale_period = iniData["dos"]["locale_period"],
                DOS_country = iniData["dos"]["country"],
                DOS_keyboardlayout = iniData["dos"]["keyboardlayout"],
                DOS_expand_shell_variable = iniData["dos"]["expand_shell_variable"],
                DOS_shell_history_file = iniData["dos"]["shell_history_file"],
                DOS_setver_table_file = iniData["dos"]["setver_table_file"],
                DOS_pcjr_memory_config = iniData["dos"]["pcjr_memory_config"],

                // [ipx]
                IPX_ipx = iniData["ipx"]["ipx"],

                // [ethernet]
                ETHERNET_ne2000 = iniData["ethernet"]["ne2000"],
                ETHERNET_nicbase = iniData["ethernet"]["nicbase"],
                ETHERNET_nicirq = iniData["ethernet"]["nicirq"],
                ETHERNET_macaddr = iniData["ethernet"]["macaddr"],
                ETHERNET_tcp_port_forwards = iniData["ethernet"]["tcp_port_forwards"],
                ETHERNET_udp_port_forwards = iniData["ethernet"]["udp_port_forwards"],

                // [autoexec]
                // It is neccecary to handle this section manually
                //AUTOEXEC_autoexec = iniData["autoexec"]["autoexec"]
            };
            return userConf;
        }

        private void NewUserConf()
        {
            // If paths are not set, return
            if (!PathsAreSet())
            {
                return;
            }

            // Open a file dialog with the user conf folder as the initial directory to let the user choose the name and location of the new user conf
            SaveFileDialog saveFileDialog = new()
            {
                InitialDirectory = Properties.Settings.Default.UserConfFolderPath,
                Filter = "Conf files (*.conf)|*.conf",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string userConfFilePath = saveFileDialog.FileName;

                // Create the new user conf file
                File.Create(userConfFilePath).Close();

                // Add the new user conf to the listbox
                ListBoxUserConfs.Items.Add(Path.GetFileName(userConfFilePath));
                ListBoxUserConfs.SelectedItem = Path.GetFileName(userConfFilePath);
            }
        }

        private void Save()
        {
            bool showWarning = Properties.Settings.Default.ShowSaveConfWaring;

            // If there isn't anything selected in the listbox, return
            if (ListBoxUserConfs.SelectedItem == null)
            {
                throw new Exception("Please select a user conf file from the listbox.");
            }

            string? userConfFileName = ListBoxUserConfs.SelectedItem.ToString();
            if (string.IsNullOrEmpty(userConfFileName))
            {
                throw new Exception("Invalid user conf selected. Please check the paths in the options.");
            }

            // If paths are not set, return
            if (!PathsAreSet())
            {
                return;
            }

            string userConfFilePath = Path.Combine(Properties.Settings.Default.UserConfFolderPath, userConfFileName);
            string globalConfFilePath = Properties.Settings.Default.GlobalConfFilePath;

            // Save the user conf (the global conf is needed to see what properties are different from global properties)
            SaveConf(showWarning, userConfFilePath, globalConfFilePath);
            // Save the autoexec section
            SaveAutoexecSection(userConfFilePath);
        }

        private void SaveAs()
        {
            // If there isn't anything selected in the listbox, return
            if (ListBoxUserConfs.SelectedItem == null)
            {
                throw new Exception("Please select a user conf file from the listbox.");
            }

            string? userConfFileName = ListBoxUserConfs.SelectedItem.ToString();
            if (string.IsNullOrEmpty(userConfFileName))
            {
                throw new Exception("Invalid user conf selected. Please check the paths in the options.");
            }

            // If paths are not set, return
            if (!PathsAreSet())
            {
                return;
            }

            // Open a new FormSaveAs to let the user choose the name of the new user conf
            string newFileName = string.Empty;
            using (var form = new FormSaveAs())
            {
                var buttonOk = form.Controls.Find("ButtonOk", true).FirstOrDefault() as Button;
                var buttonCancel = form.Controls.Find("ButtonCancel", true).FirstOrDefault() as Button;
                var textBox = form.Controls.Find("TextBoxNewConfName", true).FirstOrDefault() as TextBox;

                if (buttonOk == null || buttonCancel == null || textBox == null)
                {
                    throw new Exception("Error finding the controls in the form.");
                }

                buttonOk.Click += (sender, e) =>
                {
                    newFileName = textBox.Text.Trim();
                    // Check if the new file name has the .conf extension
                    if (!newFileName.EndsWith(".conf"))
                    {
                        newFileName += ".conf";
                    }
                    form.Close();
                };

                buttonCancel.Click += (sender, e) =>
                {
                    form.Close();
                };

                form.ShowDialog();
            }

            if (string.IsNullOrEmpty(newFileName))
            {
                return;
            }

            string userConfFilePath = Path.Combine(Properties.Settings.Default.UserConfFolderPath, userConfFileName);
            string newFilePath = Path.Combine(Properties.Settings.Default.UserConfFolderPath, newFileName);

            // Copy the user conf file to the new file
            File.Copy(userConfFilePath, newFilePath);

            // Add the new user conf to the listbox
            ListBoxUserConfs.Items.Add(newFileName);
            ListBoxUserConfs.SelectedItem = newFileName;
        }

        private void Delete()
        {
            // If there isn't anything selected in the listbox, return
            if (ListBoxUserConfs.SelectedItem == null)
            {
                throw new Exception("Please select a user conf file from the listbox.");
            }

            string? userConfFileName = ListBoxUserConfs.SelectedItem.ToString();
            if (string.IsNullOrEmpty(userConfFileName))
            {
                throw new Exception("Invalid user conf selected. Please check the paths in the options.");
            }

            // If paths are not set, return
            if (!PathsAreSet())
            {
                return;
            }

            string userConfFilePath = Path.Combine(Properties.Settings.Default.UserConfFolderPath, userConfFileName);

            // Ask the user if they are sure they want to delete the user conf
            DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete the conf file {userConfFileName}?", "Delete user conf", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                // Delete the user conf file
                File.Delete(userConfFilePath);

                // Remove the user conf from the listbox
                ListBoxUserConfs.Items.Remove(userConfFileName);
            }
        }

        private void Launch(string? parameters)
        {
            // If there isn't anything selected in the listbox, return
            if (ListBoxUserConfs.SelectedItem == null)
            {
                throw new Exception("Please select a conf file from the listbox.");
            }

            string? userConfFileName = ListBoxUserConfs.SelectedItem.ToString();
            if (string.IsNullOrEmpty(userConfFileName))
            {
                throw new Exception("Invalid conf selected. Please check the paths in the options.");
            }

            // If paths are not set, return
            if (!PathsAreSet())
            {
                return;
            }

            string userConfFilePath = Path.Combine(Properties.Settings.Default.UserConfFolderPath, userConfFileName);
            string dosBoxPath = Properties.Settings.Default.DosBoxStagingExeFilePath;

            // Launch the game
            if (string.IsNullOrEmpty(parameters))
            {
                // Launch without parameters
                Process.Start(dosBoxPath, $" -conf \"{userConfFilePath}\"");
            }
            else
            {
                // Launch with parameters
                Process.Start(dosBoxPath, $"{parameters} -conf \"{userConfFilePath}\"");
            }
        }

        private void SetLanguage()
        {
            string lang = "en-GB"; // TO DO --> Set the language in options

            switch (lang)
            {
                case "en-GB":
                    resourceManager = Language_en_GB.ResourceManager;
                    break;

                case "es-ES":
                    resourceManager = Language_es_ES.ResourceManager;
                    break;
            }
        }

        private void SetToolTips()
        {
            EventHandler handler = new((sender, e) => LabelMouseHover(sender ?? this, e));
            foreach (TabPage tabPage in TabControlOptions.TabPages)
            {
                foreach (Control control in tabPage.Controls)
                {
                    if (control is Label)
                    {
                        control.MouseHover += handler;
                    }
                }
            }
        }

        private void LabelMouseHover(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                string key = label.Name.Replace("Label", "Help");
                ToolTipHelp.SetToolTip(label, resourceManager.GetString(key));
            }
        }

        private void SetComboBoxOptions(string jsonFilePath)
        {
            // This method is used to set the options of the comboboxes from a JSON file

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException();
            }

            string jsonComboBoxOptionsFile = File.ReadAllText(jsonFilePath);
            Dictionary<string, List<Option>>? comboBoxOptions = JsonConvert.DeserializeObject<Dictionary<string, List<Option>>>(jsonComboBoxOptionsFile);

            if (comboBoxOptions != null)
            {
                foreach (var comboBoxOption in comboBoxOptions)
                {
                    var control = Controls.Find(comboBoxOption.Key, true).FirstOrDefault();
                    if (control is ComboBox comboBox)
                    {
                        comboBox.Items.Clear();
                        comboBox.Items.AddRange(comboBoxOption.Value.ToArray());
                        comboBox.DisplayMember = "Text";
                        comboBox.ValueMember = "Value";
                    }
                }
            }
            else
            {
                throw new Exception("Error deserializing the JSON file");
            }
        }

        private void LoadConf(GlobalConf globalConf, UserConf userConf)
        {
            // [sdl]
            LoadOption(globalConf.SDL_fullscreen, userConf.SDL_fullscreen, ComboBoxSdlFullscreen, null, LabelSdlFullscreen);
            LoadOption(globalConf.SDL_display, userConf.SDL_display, ComboBoxSdlDisplay, null, LabelSdlDisplay);
            LoadOption(globalConf.SDL_fullresolution, userConf.SDL_fullresolution, ComboBoxSdlFullresolution, TextBoxSdlFullresolution, LabelSdlFullresolution);
            LoadOption(globalConf.SDL_windowresolution, userConf.SDL_windowresolution, ComboBoxSdlWindowresolution, TextBoxSdlWindowresolution, LabelSdlWindowresolution);
            LoadOption(globalConf.SDL_window_position, userConf.SDL_window_position, ComboBoxSdlWindowPosition, TextBoxSdlWindowPosition, LabelSdlWindowPosition);
            LoadOption(globalConf.SDL_window_decorations, userConf.SDL_window_decorations, ComboBoxSdlWindowDecorations, null, LabelSdlWindowDecorations);
            LoadOption(globalConf.SDL_transparency, userConf.SDL_transparency, ComboBoxSdlTransparency, null, LabelSdlTransparency);
            LoadOption(globalConf.SDL_host_rate, userConf.SDL_host_rate, ComboBoxSdlHostRate, TextBoxSdlHostRate, LabelSdlHostRate);
            LoadOption(globalConf.SDL_vsync, userConf.SDL_vsync, ComboBoxSdlVsync, null, LabelSdlVsync);
            LoadOption(globalConf.SDL_vsync_skip, userConf.SDL_vsync_skip, null, TextBoxSdlVsyncSkip, LabelSdlVsyncSkip);
            LoadOption(globalConf.SDL_presentation_mode, userConf.SDL_presentation_mode, ComboBoxSdlPresentationMode, null, LabelSdlPresentationMode);
            LoadOption(globalConf.SDL_output, userConf.SDL_output, ComboBoxSdlOutput, null, LabelSdlOutput);
            LoadOption(globalConf.SDL_texture_renderer, userConf.SDL_texture_renderer, ComboBoxSdlTextureRenderer, null, LabelSdlTextureRenderer);
            LoadOption(globalConf.SDL_waitonerror, userConf.SDL_waitonerror, ComboBoxSdlWaitonerror, null, LabelSdlWaitonerror);
            LoadOption(globalConf.SDL_priority.Split(' ')[0], userConf.SDL_priority?.Split(' ')[0], ComboBoxSdlPriorityActive, null, LabelSdlPriorityActive);
            LoadOption(globalConf.SDL_priority.Split(' ')[1], userConf.SDL_priority?.Split(' ')[1], ComboBoxSdlPriorityInactive, null, LabelSdlPriorityInactive);
            LoadOption(globalConf.SDL_mute_when_inactive, userConf.SDL_mute_when_inactive, ComboBoxSdlMuteWhenInactive, null, LabelSdlMuteWhenInactive);
            LoadOption(globalConf.SDL_pause_when_inactive, userConf.SDL_pause_when_inactive, ComboBoxSdlPauseWhenInactive, null, LabelSdlPauseWhenInactive);
            LoadOption(globalConf.SDL_mapperfile, userConf.SDL_mapperfile, null, TextBoxSdlMapperfile, LabelSdlMapperfile);
            LoadOption(globalConf.SDL_screensaver, userConf.SDL_screensaver, ComboBoxSdlScreensaver, null, LabelSdlScreensaver);

            // [dosbox]
            LoadOption(globalConf.DOSBOX_language, userConf.DOSBOX_language, ComboBoxDosboxLanguage, null, LabelDosboxLanguage);
            LoadOption(globalConf.DOSBOX_machine, userConf.DOSBOX_machine, ComboBoxDosboxMachine, null, LabelDosboxMachine);
            LoadOption(globalConf.DOSBOX_memsize, userConf.DOSBOX_memsize, ComboBoxDosboxMemsize, null, LabelDosboxMemsize);
            LoadOption(globalConf.DOSBOX_mcb_fault_strategy, userConf.DOSBOX_mcb_fault_strategy, ComboBoxDosboxMcbFaultStrategy, null, LabelDosboxMcbFaultStrategy);
            LoadOption(globalConf.DOSBOX_vmemsize, userConf.DOSBOX_vmemsize, ComboBoxDosboxVmemsize, null, LabelDosboxVmemsize);
            LoadOption(globalConf.DOSBOX_vmem_delay, userConf.DOSBOX_vmem_delay, ComboBoxDosboxVmemDelay, TextBoxDosboxVmemDelay, LabelDosboxVmemDelay);
            LoadOption(globalConf.DOSBOX_dos_rate, userConf.DOSBOX_dos_rate, ComboBoxDosboxDosRate, TextBoxDosboxDosRate, LabelDosboxDosRate);
            LoadOption(globalConf.DOSBOX_vesa_modes, userConf.DOSBOX_vesa_modes, ComboBoxDosboxVesaModes, null, LabelDosboxVesaModes);
            LoadOption(globalConf.DOSBOX_vga_8dot_font, userConf.DOSBOX_vga_8dot_font, ComboBoxDosboxVga8dotFont, null, LabelDosboxVga8dotFont);
            LoadOption(globalConf.DOSBOX_vga_render_per_scanline, userConf.DOSBOX_vga_render_per_scanline, ComboBoxDosboxVgaRenderPerScanline, null, LabelDosboxVgaRenderPerScanline);
            LoadOption(globalConf.DOSBOX_speed_mods, userConf.DOSBOX_speed_mods, ComboBoxDosboxSpeedMods, null, LabelDosboxSpeedMods);
            LoadOption(globalConf.DOSBOX_autoexec_section, userConf.DOSBOX_autoexec_section, ComboBoxDosboxAutoexecSection, null, LabelDosboxAutoexecSection);
            LoadOption(globalConf.DOSBOX_automount, userConf.DOSBOX_automount, ComboBoxDosboxAutomount, null, LabelDosboxAutomount);
            LoadOption(globalConf.DOSBOX_startup_verbosity, userConf.DOSBOX_startup_verbosity, ComboBoxDosboxStartupVerbosity, null, LabelDosboxStartupVerbosity);
            LoadOption(globalConf.DOSBOX_allow_write_protected_files, userConf.DOSBOX_allow_write_protected_files, ComboBoxDosboxAllowWriteProtectedFiles, null, LabelDosboxAllowWriteProtectedFiles);
            LoadOption(globalConf.DOSBOX_shell_config_shortcuts, userConf.DOSBOX_shell_config_shortcuts, ComboBoxDosboxShellConfigShortcuts, null, LabelDosboxShellConfigShortcuts);

            // [render]
            LoadOption(globalConf.RENDER_aspect, userConf.RENDER_aspect, ComboBoxRenderAspect, null, LabelRenderAspect);
            LoadOption(globalConf.RENDER_integer_scaling, userConf.RENDER_integer_scaling, ComboBoxRenderIntegerScaling, null, LabelRenderIntegerScaling);
            LoadOption(globalConf.RENDER_viewport, userConf.RENDER_viewport, ComboBoxRenderViewport, TextBoxRenderViewport, LabelRenderViewport);
            LoadOption(globalConf.RENDER_monochrome_palette, userConf.RENDER_monochrome_palette, ComboBoxRenderMonochromePalette, null, LabelRenderMonochromePalette);
            LoadOption(globalConf.RENDER_cga_colors, userConf.RENDER_cga_colors, ComboBoxRenderCgaColors, TextBoxRenderCgaColors, LabelRenderCgaColors);
            LoadOption(globalConf.RENDER_glshader, userConf.RENDER_glshader, ComboBoxRenderGlshader, TextBoxRenderGlshader, LabelRenderGlshader);

            // [composite]
            LoadOption(globalConf.COMPOSITE_composite, userConf.COMPOSITE_composite, ComboBoxCompositeComposite, null, LabelCompositeComposite);
            LoadOption(globalConf.COMPOSITE_era, userConf.COMPOSITE_era, ComboBoxCompositeEra, null, LabelCompositeEra);
            LoadOption(globalConf.COMPOSITE_hue, userConf.COMPOSITE_hue, null, TextBoxCompositeHue, LabelCompositeHue);
            LoadOption(globalConf.COMPOSITE_saturation, userConf.COMPOSITE_saturation, null, TextBoxCompositeSaturation, LabelCompositeSaturation);
            LoadOption(globalConf.COMPOSITE_contrast, userConf.COMPOSITE_contrast, null, TextBoxCompositeContrast, LabelCompositeContrast);
            LoadOption(globalConf.COMPOSITE_brightness, userConf.COMPOSITE_brightness, null, TextBoxCompositeBrightness, LabelCompositeBrightness);
            LoadOption(globalConf.COMPOSITE_convergence, userConf.COMPOSITE_convergence, null, TextBoxCompositeConvergence, LabelCompositeConvergence);

            // [cpu]
            LoadOption(globalConf.CPU_core, userConf.CPU_core, ComboBoxCpuCore, null, LabelCpuCore);
            LoadOption(globalConf.CPU_cputype, userConf.CPU_cputype, ComboBoxCpuCputype, null, LabelCpuCputype);
            LoadOption(globalConf.CPU_cycles, userConf.CPU_cycles, ComboBoxCpuCycles, TextBoxCpuCycles, LabelCpuCycles);
            LoadOption(globalConf.CPU_cycleup, userConf.CPU_cycleup, null, TextBoxCpuCycleup, LabelCpuCycleup);
            LoadOption(globalConf.CPU_cycledown, userConf.CPU_cycledown, null, TextBoxCpuCycledown, LabelCpuCycledown);

            // [voodoo]
            LoadOption(globalConf.VOODOO_voodoo, userConf.VOODOO_voodoo, ComboBoxVoodooVoodoo, null, LabelVoodooVoodoo);
            LoadOption(globalConf.VOODOO_voodoo_memsize, userConf.VOODOO_voodoo_memsize, ComboBoxVoodooVoodooMemsize, null, LabelVoodooVoodooMemsize);
            LoadOption(globalConf.VOODOO_voodoo_multithreading, userConf.VOODOO_voodoo_multithreading, ComboBoxVoodooVoodooMultithreading, null, LabelVoodooVoodooMultithreading);
            LoadOption(globalConf.VOODOO_voodoo_bilinear_filtering, userConf.VOODOO_voodoo_bilinear_filtering, ComboBoxVoodooVoodooBilinearFiltering, null, LabelVoodooVoodooBilinearFiltering);

            // [capture]
            LoadOption(globalConf.CAPTURE_capture_dir, userConf.CAPTURE_capture_dir, null, TextBoxCaptureCaptureDir, LabelCaptureCaptureDir);
            LoadOption(globalConf.CAPTURE_default_image_capture_formats, userConf.CAPTURE_default_image_capture_formats, ComboBoxCaptureDefaultImageCaptureFormats, null, LabelCaptureDefaultImageCaptureFormats);

            // [mouse]
            LoadOption(globalConf.MOUSE_mouse_capture, userConf.MOUSE_mouse_capture, ComboBoxMouseMouseCapture, null, LabelMouseMouseCapture);
            LoadOption(globalConf.MOUSE_mouse_middle_release, userConf.MOUSE_mouse_middle_release, ComboBoxMouseMouseMiddleRelease, null, LabelMouseMouseMiddleRelease);
            LoadOption(globalConf.MOUSE_mouse_multi_display_aware, userConf.MOUSE_mouse_multi_display_aware, ComboBoxMouseMouseMultiDisplayAware, null, LabelMouseMouseMultiDisplayAware);
            LoadOption(globalConf.MOUSE_mouse_sensitivity, userConf.MOUSE_mouse_sensitivity, null, TextBoxMouseMouseSensitivity, LabelMouseMouseSensitivity);
            LoadOption(globalConf.MOUSE_mouse_raw_input, userConf.MOUSE_mouse_raw_input, ComboBoxMouseMouseRawInput, null, LabelMouseMouseRawInput);
            LoadOption(globalConf.MOUSE_dos_mouse_driver, userConf.MOUSE_dos_mouse_driver, ComboBoxMouseDosMouseDriver, null, LabelMouseDosMouseDriver);
            LoadOption(globalConf.MOUSE_dos_mouse_immediate, userConf.MOUSE_dos_mouse_immediate, ComboBoxMouseDosMouseImmediate, null, LabelMouseDosMouseImmediate);
            LoadOption(globalConf.MOUSE_ps2_mouse_model, userConf.MOUSE_ps2_mouse_model, ComboBoxMousePs2MouseModel, null, LabelMousePs2MouseModel);
            LoadOption(globalConf.MOUSE_com_mouse_model, userConf.MOUSE_com_mouse_model, ComboBoxMouseComMouseModel, null, LabelMouseComMouseModel);
            LoadOption(globalConf.MOUSE_vmware_mouse, userConf.MOUSE_vmware_mouse, ComboBoxMouseVmwareMouse, null, LabelMouseVmwareMouse);
            LoadOption(globalConf.MOUSE_virtualbox_mouse, userConf.MOUSE_virtualbox_mouse, ComboBoxMouseVirtualboxMouse, null, LabelMouseVirtualboxMouse);

            // [mixer]
            LoadOption(globalConf.MIXER_nosound, userConf.MIXER_nosound, ComboBoxMixerNosound, null, LabelMixerNosound);
            LoadOption(globalConf.MIXER_rate, userConf.MIXER_rate, ComboBoxMixerRate, null, LabelMixerRate);
            LoadOption(globalConf.MIXER_blocksize, userConf.MIXER_blocksize, ComboBoxMixerBlocksize, null, LabelMixerBlocksize);
            LoadOption(globalConf.MIXER_prebuffer, userConf.MIXER_prebuffer, null, TextBoxMixerPrebuffer, LabelMixerPrebuffer);
            LoadOption(globalConf.MIXER_negotiate, userConf.MIXER_negotiate, ComboBoxMixerNegotiate, null, LabelMixerNegotiate);
            LoadOption(globalConf.MIXER_compressor, userConf.MIXER_compressor, ComboBoxMixerCompressor, null, LabelMixerCompressor);
            LoadOption(globalConf.MIXER_crossfeed, userConf.MIXER_crossfeed, ComboBoxMixerCrossfeed, null, LabelMixerCrossfeed);
            LoadOption(globalConf.MIXER_reverb, userConf.MIXER_reverb, ComboBoxMixerReverb, null, LabelMixerReverb);
            LoadOption(globalConf.MIXER_chorus, userConf.MIXER_chorus, ComboBoxMixerChorus, null, LabelMixerChorus);

            // [midi]
            LoadOption(globalConf.MIDI_mididevice, userConf.MIDI_mididevice, ComboBoxMidiMididevice, null, LabelMidiMididevice);
            LoadOption(globalConf.MIDI_midiconfig, userConf.MIDI_midiconfig, null, TextBoxMidiMidiconfig, LabelMidiMidiconfig);
            LoadOption(globalConf.MIDI_mpu401, userConf.MIDI_mpu401, ComboBoxMidiMpu401, null, LabelMidiMpu401);
            LoadOption(globalConf.MIDI_raw_midi_output, userConf.MIDI_raw_midi_output, ComboBoxMidiRawMidiOutput, null, LabelMidiRawMidiOutput);

            // [fluidsynth]
            LoadOption(globalConf.FLUIDSYNTH_soundfont, userConf.FLUIDSYNTH_soundfont, null, TextBoxFluidsynthSoundfont, LabelFluidsynthSoundfont);
            LoadOption(globalConf.FLUIDSYNTH_fsynth_chorus, userConf.FLUIDSYNTH_fsynth_chorus, ComboBoxFluidsynthFsynthChorus, TextBoxFluidsynthFsynthChorus, LabelFluidsynthFsynthChorus);
            LoadOption(globalConf.FLUIDSYNTH_fsynth_reverb, userConf.FLUIDSYNTH_fsynth_reverb, ComboBoxFluidsynthFsynthReverb, TextBoxFluidsynthFsynthReverb, LabelFluidsynthFsynthReverb);
            LoadOption(globalConf.FLUIDSYNTH_fsynth_filter, userConf.FLUIDSYNTH_fsynth_filter, ComboBoxFluidsynthFsynthFilter, TextBoxFluidsynthFsynthFilter, LabelFluidsynthFsynthFilter);

            // [mt32]
            LoadOption(globalConf.MT32_model, userConf.MT32_model, ComboBoxMt32Model, null, LabelMt32Model);
            LoadOption(globalConf.MT32_romdir, userConf.MT32_romdir, null, TextBoxMt32Romdir, LabelMt32Romdir);
            LoadOption(globalConf.MT32_mt32_filter, userConf.MT32_mt32_filter, ComboBoxMt32Mt32Filter, TextBoxMt32Mt32Filter, LabelMt32Mt32Filter);

            // [sblaster]
            LoadOption(globalConf.SBLASTER_sbtype, userConf.SBLASTER_sbtype, ComboBoxSblasterSbtype, null, LabelSblasterSbtype);
            LoadOption(globalConf.SBLASTER_sbbase, userConf.SBLASTER_sbbase, ComboBoxSblasterSbbase, null, LabelSblasterSbbase);
            LoadOption(globalConf.SBLASTER_irq, userConf.SBLASTER_irq, ComboBoxSblasterIrq, null, LabelSblasterIrq);
            LoadOption(globalConf.SBLASTER_dma, userConf.SBLASTER_dma, ComboBoxSblasterDma, null, LabelSblasterDma);
            LoadOption(globalConf.SBLASTER_hdma, userConf.SBLASTER_hdma, ComboBoxSblasterHdma, null, LabelSblasterHdma);
            LoadOption(globalConf.SBLASTER_sbmixer, userConf.SBLASTER_sbmixer, ComboBoxSblasterSbmixer, null, LabelSblasterSbmixer);
            LoadOption(globalConf.SBLASTER_sbwarmup, userConf.SBLASTER_sbwarmup, null, TextBoxSblasterSbwarmup, LabelSblasterSbwarmup);
            LoadOption(globalConf.SBLASTER_oplmode, userConf.SBLASTER_oplmode, ComboBoxSblasterOplmode, null, LabelSblasterOplmode);
            LoadOption(globalConf.SBLASTER_opl_fadeout, userConf.SBLASTER_opl_fadeout, ComboBoxSblasterOplFadeout, TextBoxSblasterOplFadeout, LabelSblasterOplFadeout);
            LoadOption(globalConf.SBLASTER_sb_filter, userConf.SBLASTER_sb_filter, ComboBoxSblasterSbFilter, TextBoxSblasterSbFilter, LabelSblasterSbFilter);
            LoadOption(globalConf.SBLASTER_sb_filter_always_on, userConf.SBLASTER_sb_filter_always_on, ComboBoxSblasterSbFilterAlwaysOn, null, LabelSblasterSbFilterAlwaysOn);
            LoadOption(globalConf.SBLASTER_opl_filter, userConf.SBLASTER_opl_filter, ComboBoxSblasterOplFilter, TextBoxSblasterOplFilter, LabelSblasterOplFilter);
            LoadOption(globalConf.SBLASTER_cms_filter, userConf.SBLASTER_cms_filter, ComboBoxSblasterCmsFilter, TextBoxSblasterCmsFilter, LabelSblasterCmsFilter);

            // [gus]
            LoadOption(globalConf.GUS_gus, userConf.GUS_gus, ComboBoxGusGus, null, LabelGusGus);
            LoadOption(globalConf.GUS_gusbase, userConf.GUS_gusbase, ComboBoxGusGusbase, null, LabelGusGusbase);
            LoadOption(globalConf.GUS_gusirq, userConf.GUS_gusirq, ComboBoxGusGusirq, null, LabelGusGusirq);
            LoadOption(globalConf.GUS_gusdma, userConf.GUS_gusdma, ComboBoxGusGusdma, null, LabelGusGusdma);
            LoadOption(globalConf.GUS_ultradir, userConf.GUS_ultradir, null, TextBoxGusUltradir, LabelGusUltradir);
            LoadOption(globalConf.GUS_gus_filter, userConf.GUS_gus_filter, ComboBoxGusGusFilter, TextBoxGusGusFilter, LabelGusGusFilter);

            // [imfc]
            LoadOption(globalConf.IMFC_imfc, userConf.IMFC_imfc, ComboBoxImfcImfc, null, LabelImfcImfc);
            LoadOption(globalConf.IMFC_imfc_base, userConf.IMFC_imfc_base, ComboBoxImfcImfcBase, null, LabelImfcImfcBase);
            LoadOption(globalConf.IMFC_imfc_irq, userConf.IMFC_imfc_irq, ComboBoxImfcImfcIrq, null, LabelImfcImfcIrq);
            LoadOption(globalConf.IMFC_imfc_filter, userConf.IMFC_imfc_filter, ComboBoxImfcImfcFilter, TextBoxImfcImfcFilter, LabelImfcImfcFilter);

            // [innovation]
            LoadOption(globalConf.INNOVATION_sidmodel, userConf.INNOVATION_sidmodel, ComboBoxInnovationSidmodel, null, LabelInnovationSidmodel);
            LoadOption(globalConf.INNOVATION_sidclock, userConf.INNOVATION_sidclock, ComboBoxInnovationSidclock, null, LabelInnovationSidclock);
            LoadOption(globalConf.INNOVATION_sidport, userConf.INNOVATION_sidport, ComboBoxInnovationSidport, null, LabelInnovationSidport);
            LoadOption(globalConf.INNOVATION_6581filter, userConf.INNOVATION_6581filter, null, TextBoxInnovation6581filter, LabelInnovation6581filter);
            LoadOption(globalConf.INNOVATION_8580filter, userConf.INNOVATION_8580filter, null, TextBoxInnovation8580filter, LabelInnovation8580filter);
            LoadOption(globalConf.INNOVATION_innovation_filter, userConf.INNOVATION_innovation_filter, ComboBoxInnovationInnovationFilter, TextBoxInnovationInnovationFilter, LabelInnovationInnovationFilter);

            // [speaker]
            LoadOption(globalConf.SPEAKER_pcspeaker, userConf.SPEAKER_pcspeaker, ComboBoxSpeakerPcspeaker, null, LabelSpeakerPcspeaker);
            LoadOption(globalConf.SPEAKER_pcspeaker_filter, userConf.SPEAKER_pcspeaker_filter, ComboBoxSpeakerPcspeakerFilter, TextBoxSpeakerPcspeakerFilter, LabelSpeakerPcspeakerFilter);
            LoadOption(globalConf.SPEAKER_tandy, userConf.SPEAKER_tandy, ComboBoxSpeakerTandy, null, LabelSpeakerTandy);
            LoadOption(globalConf.SPEAKER_tandy_fadeout, userConf.SPEAKER_tandy_fadeout, ComboBoxSpeakerTandyFadeout, TextBoxSpeakerTandyFadeout, LabelSpeakerTandyFadeout);
            LoadOption(globalConf.SPEAKER_tandy_filter, userConf.SPEAKER_tandy_filter, ComboBoxSpeakerTandyFilter, TextBoxSpeakerTandyFilter, LabelSpeakerTandyFilter);
            LoadOption(globalConf.SPEAKER_tandy_dac_filter, userConf.SPEAKER_tandy_dac_filter, ComboBoxSpeakerTandyDacFilter, TextBoxSpeakerTandyDacFilter, LabelSpeakerTandyDacFilter);
            LoadOption(globalConf.SPEAKER_lpt_dac, userConf.SPEAKER_lpt_dac, ComboBoxSpeakerLptDac, null, LabelSpeakerLptDac);
            LoadOption(globalConf.SPEAKER_lpt_dac_filter, userConf.SPEAKER_lpt_dac_filter, ComboBoxSpeakerLptDacFilter, TextBoxSpeakerLptDacFilter, LabelSpeakerLptDacFilter);
            LoadOption(globalConf.SPEAKER_ps1audio, userConf.SPEAKER_ps1audio, ComboBoxSpeakerPs1audio, null, LabelSpeakerPs1audio);
            LoadOption(globalConf.SPEAKER_ps1audio_filter, userConf.SPEAKER_ps1audio_filter, ComboBoxSpeakerPs1audioFilter, TextBoxSpeakerPs1audioFilter, LabelSpeakerPs1audioFilter);
            LoadOption(globalConf.SPEAKER_ps1audio_dac_filter, userConf.SPEAKER_ps1audio_dac_filter, ComboBoxSpeakerPs1audioDacFilter, TextBoxSpeakerPs1audioDacFilter, LabelSpeakerPs1audioDacFilter);

            // [reelmagic]
            LoadOption(globalConf.REELMAGIC_reelmagic, userConf.REELMAGIC_reelmagic, ComboBoxReelmagicReelmagic, null, LabelReelmagicReelmagic);
            LoadOption(globalConf.REELMAGIC_reelmagic_key, userConf.REELMAGIC_reelmagic_key, ComboBoxReelmagicReelmagicKey, TextBoxReelmagicReelmagicKey, LabelReelmagicReelmagicKey);
            LoadOption(globalConf.REELMAGIC_reelmagic_fcode, userConf.REELMAGIC_reelmagic_fcode, ComboBoxReelmagicReelmagicFcode, null, LabelReelmagicReelmagicFcode);

            // [joystick]
            LoadOption(globalConf.JOYSTICK_joysticktype, userConf.JOYSTICK_joysticktype, ComboBoxJoystickJoysticktype, null, LabelJoystickJoysticktype);
            LoadOption(globalConf.JOYSTICK_timed, userConf.JOYSTICK_timed, ComboBoxJoystickTimed, null, LabelJoystickTimed);
            LoadOption(globalConf.JOYSTICK_autofire, userConf.JOYSTICK_autofire, ComboBoxJoystickAutofire, null, LabelJoystickAutofire);
            LoadOption(globalConf.JOYSTICK_swap34, userConf.JOYSTICK_swap34, ComboBoxJoystickSwap34, null, LabelJoystickSwap34);
            LoadOption(globalConf.JOYSTICK_buttonwrap, userConf.JOYSTICK_buttonwrap, ComboBoxJoystickButtonwrap, null, LabelJoystickButtonwrap);
            LoadOption(globalConf.JOYSTICK_circularinput, userConf.JOYSTICK_circularinput, ComboBoxJoystickCircularinput, null, LabelJoystickCircularinput);
            LoadOption(globalConf.JOYSTICK_deadzone, userConf.JOYSTICK_deadzone, null, TextBoxJoystickDeadzone, LabelJoystickDeadzone);
            LoadOption(globalConf.JOYSTICK_use_joy_calibration_hotkeys, userConf.JOYSTICK_use_joy_calibration_hotkeys, ComboBoxJoystickUseJoyCalibrationHotkeys, null, LabelJoystickUseJoyCalibrationHotkeys);
            LoadOption(globalConf.JOYSTICK_joy_x_calibration, userConf.JOYSTICK_joy_x_calibration, ComboBoxJoystickJoyXCalibration, TextBoxJoystickJoyXCalibration, LabelJoystickJoyXCalibration);
            LoadOption(globalConf.JOYSTICK_joy_y_calibration, userConf.JOYSTICK_joy_y_calibration, ComboBoxJoystickJoyYCalibration, TextBoxJoystickJoyYCalibration, LabelJoystickJoyYCalibration);

            // [serial]
            LoadOption(globalConf.SERIAL_serial1, userConf.SERIAL_serial1, ComboBoxSerialSerial1, TextBoxSerialSerial1, LabelSerialSerial1);
            LoadOption(globalConf.SERIAL_serial2, userConf.SERIAL_serial2, ComboBoxSerialSerial2, TextBoxSerialSerial2, LabelSerialSerial2);
            LoadOption(globalConf.SERIAL_serial3, userConf.SERIAL_serial3, ComboBoxSerialSerial3, TextBoxSerialSerial3, LabelSerialSerial3);
            LoadOption(globalConf.SERIAL_serial4, userConf.SERIAL_serial4, ComboBoxSerialSerial4, TextBoxSerialSerial4, LabelSerialSerial4);
            LoadOption(globalConf.SERIAL_phonebookfile, userConf.SERIAL_phonebookfile, null, TextBoxSerialPhonebookfile, LabelSerialPhonebookfile);

            // [dos]
            LoadOption(globalConf.DOS_xms, userConf.DOS_xms, ComboBoxDosXms, null, LabelDosXms);
            LoadOption(globalConf.DOS_ems, userConf.DOS_ems, ComboBoxDosEms, null, LabelDosEms);
            LoadOption(globalConf.DOS_umb, userConf.DOS_umb, ComboBoxDosUmb, null, LabelDosUmb);
            LoadOption(globalConf.DOS_ver, userConf.DOS_ver, ComboBoxDosVer, TextBoxDosVer, LabelDosVer);
            LoadOption(globalConf.DOS_locale_period, userConf.DOS_locale_period, ComboBoxDosLocalePeriod, null, LabelDosLocalePeriod);
            LoadOption(globalConf.DOS_country, userConf.DOS_country, ComboBoxDosCountry, TextBoxDosCountry, LabelDosCountry);
            LoadOption(globalConf.DOS_keyboardlayout, userConf.DOS_keyboardlayout, ComboBoxDosKeyboardlayout, TextBoxDosKeyboardlayout, LabelDosKeyboardlayout);
            LoadOption(globalConf.DOS_expand_shell_variable, userConf.DOS_expand_shell_variable, ComboBoxDosExpandShellVariable, null, LabelDosExpandShellVariable);
            LoadOption(globalConf.DOS_shell_history_file, userConf.DOS_shell_history_file, null, TextBoxDosShellHistoryFile, LabelDosShellHistoryFile);
            LoadOption(globalConf.DOS_setver_table_file, userConf.DOS_setver_table_file, null, TextBoxDosSetverTableFile, LabelDosSetverTableFile);
            LoadOption(globalConf.DOS_pcjr_memory_config, userConf.DOS_pcjr_memory_config, ComboBoxDosPcjrMemoryConfig, null, LabelDosPcjrMemoryConfig);

            // [ipx]
            LoadOption(globalConf.IPX_ipx, userConf.IPX_ipx, ComboBoxIpxIpx, null, LabelIpxIpx);

            // [ethernet]
            LoadOption(globalConf.ETHERNET_ne2000, userConf.ETHERNET_ne2000, ComboBoxEthernetNe2000, null, LabelEthernetNe2000);
            LoadOption(globalConf.ETHERNET_nicbase, userConf.ETHERNET_nicbase, ComboBoxEthernetNicbase, null, LabelEthernetNicbase);
            LoadOption(globalConf.ETHERNET_nicirq, userConf.ETHERNET_nicirq, ComboBoxEthernetNicirq, null, LabelEthernetNicirq);
            LoadOption(globalConf.ETHERNET_macaddr, userConf.ETHERNET_macaddr, null, TextBoxEthernetMacaddr, LabelEthernetMacaddr);
            LoadOption(globalConf.ETHERNET_tcp_port_forwards, userConf.ETHERNET_tcp_port_forwards, null, TextBoxEthernetTcpPortForwards, LabelEthernetTcpPortForwards);
            LoadOption(globalConf.ETHERNET_udp_port_forwards, userConf.ETHERNET_udp_port_forwards, null, TextBoxEthernetUdpPortForwards, LabelEthernetUdpPortForwards);

            // [autoexec]
            // It is neccecary to handle this section manually
            //LoadOption(globalConf.AUTOEXEC_autoexec, userConf.AUTOEXEC_autoexec, null, TextBoxAutoexec, LabelAutoexec);
        }

        private static void LoadOption(string globalProperty, string? userProperty, ComboBox? comboBox, TextBox? textBox, Label label)
        {
            string property = userProperty ?? globalProperty;
            bool isUserProperty = userProperty != null;

            if (comboBox == null) // If there is no ComboBox, set the value from the TextBox
            {
                SetTextBoxValue(textBox, property);
            }
            else
            {
                // Select the option in the ComboBox
                Option? option = comboBox.Items.Cast<Option>().FirstOrDefault(o => o.Value == property);

                if (option != null)
                {
                    comboBox.SelectedItem = option;
                }
                else // If the option is not found, select the custom option
                {
                    Option? customOption = comboBox.Items.Cast<Option>().FirstOrDefault(o => o.Text.EndsWith('>'));

                    if (customOption != null) // If the selected option is a custom option
                    {
                        comboBox.SelectedItem = customOption;
                        SetTextBoxValue(textBox, property);
                    }
                }
            }
            HighlightLabelApperance(label, isUserProperty);
        }

        private void LoadAutoexecSection(string userConfFileName)
        {
            // The autoexec section is a special case because it is a multiline TextBox

            string userConfFilePath = Path.Combine(Properties.Settings.Default.UserConfFolderPath, userConfFileName);

            // First, open the user configuration file and read the autoexec section
            if (userConfFilePath == null || !File.Exists(userConfFilePath))
            {
                return;
            }

            string[] lines = File.ReadAllLines(userConfFilePath);
            string autoexecSection = string.Empty;
            bool isAutoexecSection = false;

            foreach (string line in lines)
            {
                // If the line starts with '[autoexec]', the autoexec section starts
                if (line.StartsWith("[autoexec]"))
                {
                    isAutoexecSection = true;
                    continue;
                }

                // If the autoexec section has started, read the lines until the next section starts
                if (isAutoexecSection)
                {
                    // If the line starts with '[', the autoexec section ends and stars a new section
                    if (line.StartsWith('['))
                    {
                        break;
                    }

                    // Add the line to the autoexec section
                    autoexecSection += line + Environment.NewLine;
                }
            }
            TextBoxAutoexec.Text = autoexecSection;
        }

        private static void SetTextBoxValue(TextBox? textBox, string property)
        {
            if (textBox != null)
            {
                textBox.Enabled = true;
                textBox.Text = property;
            }
        }

        private static void HighlightLabelApperance(Label label, bool highlight)
        {
            if (highlight)
            {
                //label.ForeColor = Color.FromKnownColor(KnownColor.Highlight);
                label.ForeColor = Color.FromArgb(80, 92, 206);
                label.Font = new Font(label.Font, FontStyle.Bold);
            }
            else
            {
                label.ForeColor = Color.Black;
                label.Font = new Font(label.Font, FontStyle.Regular);
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            // Assigns the corresponding TextBox based on the ComboBox's name.
            // If the ComboBox's name doesn't match any of the specified cases, null is assigned.
            TextBox? textBox = comboBox.Name switch
            {
                // [sdl]
                "ComboBoxSdlFullresolution" => TextBoxSdlFullresolution,
                "ComboBoxSdlWindowresolution" => TextBoxSdlWindowresolution,
                "ComboBoxSdlWindowPosition" => TextBoxSdlWindowPosition,
                "ComboBoxSdlHostRate" => TextBoxSdlHostRate,
                // [Dosbox]
                "ComboBoxDosboxDosRate" => TextBoxDosboxDosRate,
                "ComboBoxDosboxVmemDelay" => TextBoxDosboxVmemDelay,
                // [render]
                "ComboBoxRenderViewport" => TextBoxRenderViewport,
                "ComboBoxRenderCgaColors" => TextBoxRenderCgaColors,
                "ComboBoxRenderGlshader" => TextBoxRenderGlshader,
                // [cpu]
                "ComboBoxCpuCycles" => TextBoxCpuCycles,
                // [fluidsynth]
                "ComboBoxFluidsynthFsynthChorus" => TextBoxFluidsynthFsynthChorus,
                "ComboBoxFluidsynthFsynthReverb" => TextBoxFluidsynthFsynthReverb,
                "ComboBoxFluidsynthFsynthFilter" => TextBoxFluidsynthFsynthFilter,
                // [mt32]
                "ComboBoxMt32Mt32Filter" => TextBoxMt32Mt32Filter,
                // [sblaster]
                "ComboBoxSblasterOplFadeout" => TextBoxSblasterOplFadeout,
                "ComboBoxSblasterSbFilter" => TextBoxSblasterSbFilter,
                "ComboBoxSblasterOplFilter" => TextBoxSblasterOplFilter,
                "ComboBoxSblasterCmsFilter" => TextBoxSblasterCmsFilter,
                // [gus]
                "ComboBoxGusGusFilter" => TextBoxGusGusFilter,
                // [imfc]
                "ComboBoxImfcImfcFilter" => TextBoxImfcImfcFilter,
                // [innovation]
                "ComboBoxInnovationInnovationFilter" => TextBoxInnovationInnovationFilter,
                // [speaker]
                "ComboBoxSpeakerPcspeakerFilter" => TextBoxSpeakerPcspeakerFilter,
                "ComboBoxSpeakerTandyFadeout" => TextBoxSpeakerTandyFadeout,
                "ComboBoxSpeakerTandyFilter" => TextBoxSpeakerTandyFilter,
                "ComboBoxSpeakerTandyDacFilter" => TextBoxSpeakerTandyDacFilter,
                "ComboBoxSpeakerLptDacFilter" => TextBoxSpeakerLptDacFilter,
                "ComboBoxSpeakerPs1audioFilter" => TextBoxSpeakerPs1audioFilter,
                "ComboBoxSpeakerPs1audioDacFilter" => TextBoxSpeakerPs1audioDacFilter,
                // [reelmagic]
                "ComboBoxReelmagicReelmagicKey" => TextBoxReelmagicReelmagicKey,
                // [joystick]
                "ComboBoxJoystickJoyXCalibration" => TextBoxJoystickJoyXCalibration,
                "ComboBoxJoystickJoyYCalibration" => TextBoxJoystickJoyYCalibration,
                // [serial]
                "ComboBoxSerialSerial1" => TextBoxSerialSerial1,
                "ComboBoxSerialSerial2" => TextBoxSerialSerial2,
                "ComboBoxSerialSerial3" => TextBoxSerialSerial3,
                "ComboBoxSerialSerial4" => TextBoxSerialSerial4,
                // [dos]
                "ComboBoxDosVer" => TextBoxDosVer,
                "ComboBoxDosCountry" => TextBoxDosCountry,
                "ComboBoxDosKeyboardlayout" => TextBoxDosKeyboardlayout,
                // null
                _ => null,
            };

            // If the TextBox and the selected item in the ComboBox are not null,
            // enable the TextBox only if the text of the selected item ends with '>'.
            if (textBox != null && comboBox.SelectedItem is Option selectedOption)
            {
                textBox.Enabled = selectedOption.Text.EndsWith('>');
                textBox.Text = selectedOption.Text.EndsWith('>') ? textBox.Text : string.Empty;
            }
        }

        private static void SaveOption(ComboBox? comboBox, TextBox? textBox, Label label, string globalProperty, IniData userConfData, string section, string key)
        {
            bool isUserProperty = false;

            // Check if the value of the comboBox is the same as globalProperty

            if (comboBox == null)
            {
                if (textBox != null)
                {
                    // Options that have a TextBox
                    if (textBox.Text.Trim() == globalProperty)
                    {
                        // Do not save the property
                        isUserProperty = false;
                        userConfData[section].RemoveKey(key);
                    }
                    else
                    {
                        // Save the property
                        isUserProperty = true;
                        userConfData[section][key] = textBox.Text.Trim();
                    }
                }
            }
            else
            {
                // Check the Value in the ComboBox
                Option? option = comboBox.Items.Cast<Option>().FirstOrDefault(o => o.Text == comboBox.Text);

                if (option != null)
                {
                    // Take the value of the selected option in the ComboBox
                    if (option.Value == globalProperty)
                    {
                        // Do not save the property
                        isUserProperty = false;
                        userConfData[section].RemoveKey(key);
                    }
                    else
                    {
                        // Save the property
                        isUserProperty = true;
                        userConfData[section][key] = option.Value;
                    }
                    // Take the value of the selected option in the TextBox
                    if (option.Text.EndsWith('>'))
                    {
                        if (textBox != null)
                        {
                            if (textBox.Text.Trim() == globalProperty)
                            {
                                // Do not save the property
                                isUserProperty = false;
                                userConfData[section].RemoveKey(key);
                            }
                            else
                            {
                                // Save the property
                                isUserProperty = true;
                                userConfData[section][key] = textBox.Text.Trim();
                            }
                        }
                    }
                }
            }
            HighlightLabelApperance(label, isUserProperty);
        }

        private static void SaveOptionSplitProperty(ComboBox? comboBox1, ComboBox? comboBox2, Label label1, Label label2, string globalProperty, IniData userConfData, string section, string key)
        {
            // The property is split into two parts.
            // The first part is the value of the selected option in comboBox1 and the second part is the value of the selected option in comboBox2.
            // This occurs for example in the [sdl] section with the priority property.

            bool isUserProperty = false;

            // Check if the value of the [comboBox1 + " " + comboBox2] is the same as globalProperty

            if (comboBox1 != null && comboBox2 != null)
            {
                Option? option1 = comboBox1.Items.Cast<Option>().FirstOrDefault(o => o.Text == comboBox1.Text);
                Option? option2 = comboBox2.Items.Cast<Option>().FirstOrDefault(o => o.Text == comboBox2.Text);

                if (option1 != null && option2 != null)
                {
                    // Take the value of the selected option in the ComboBox
                    if (option1.Value + " " + option2.Value == globalProperty)
                    {
                        // Do not save the property
                        isUserProperty = false;
                        userConfData[section].RemoveKey(key);
                    }
                    else
                    {
                        // Save the property
                        isUserProperty = true;
                        userConfData[section][key] = option1.Value + " " + option2.Value;
                    }
                }
            }
            HighlightLabelApperance(label1, isUserProperty);
            HighlightLabelApperance(label2, isUserProperty);
        }

        private void SaveConf(bool showSaveConfWarning, string userConfFilePath, string globalConfFilePath)
        {
            if (showSaveConfWarning)
            {
                // TO DO --> Custom MessageBox with CheckBox to remember the user's choice
                DialogResult dialogResult = MessageBox.Show("Do you want to save the changes to the user.conf file?", "Save Changes", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            if (!TextBoxHasErrors())
            {
                var parser = new FileIniDataParser();
                parser.Parser.Configuration.CommentString = "#";
                parser.Parser.Configuration.SkipInvalidLines = true; // To process the autoexec section correctly

                // Set the path of the user.conf file to save the data
                IniData userConfData = parser.ReadFile(userConfFilePath);
                // Get the Global conf
                GlobalConf globalConf = GetGlobalConf(globalConfFilePath);

                // [sdl]
                SaveOption(ComboBoxSdlFullscreen, null, LabelSdlFullscreen, globalConf.SDL_fullscreen, userConfData, "sdl", "fullscreen");
                SaveOption(ComboBoxSdlDisplay, null, LabelSdlDisplay, globalConf.SDL_display, userConfData, "sdl", "display");
                SaveOption(ComboBoxSdlFullresolution, TextBoxSdlFullresolution, LabelSdlFullresolution, globalConf.SDL_fullresolution, userConfData, "sdl", "fullresolution");
                SaveOption(ComboBoxSdlWindowresolution, TextBoxSdlWindowresolution, LabelSdlWindowresolution, globalConf.SDL_windowresolution, userConfData, "sdl", "windowresolution");
                SaveOption(ComboBoxSdlWindowPosition, TextBoxSdlWindowPosition, LabelSdlWindowPosition, globalConf.SDL_window_position, userConfData, "sdl", "window_position");
                SaveOption(ComboBoxSdlWindowDecorations, null, LabelSdlWindowDecorations, globalConf.SDL_window_decorations, userConfData, "sdl", "window_decorations");
                SaveOption(ComboBoxSdlTransparency, null, LabelSdlTransparency, globalConf.SDL_transparency, userConfData, "sdl", "transparency");
                SaveOption(ComboBoxSdlScreensaver, null, LabelSdlScreensaver, globalConf.SDL_screensaver, userConfData, "sdl", "screensaver");
                SaveOption(ComboBoxSdlHostRate, TextBoxSdlHostRate, LabelSdlHostRate, globalConf.SDL_host_rate, userConfData, "sdl", "host_rate");
                SaveOption(ComboBoxSdlVsync, null, LabelSdlVsync, globalConf.SDL_vsync, userConfData, "sdl", "vsync");
                SaveOption(null, TextBoxSdlVsyncSkip, LabelSdlVsyncSkip, globalConf.SDL_vsync_skip, userConfData, "sdl", "vsync_skip");
                SaveOption(ComboBoxSdlPresentationMode, null, LabelSdlPresentationMode, globalConf.SDL_presentation_mode, userConfData, "sdl", "presentation_mode");
                SaveOption(ComboBoxSdlOutput, null, LabelSdlOutput, globalConf.SDL_output, userConfData, "sdl", "output");
                SaveOption(ComboBoxSdlTextureRenderer, null, LabelSdlTextureRenderer, globalConf.SDL_texture_renderer, userConfData, "sdl", "texture_renderer");
                SaveOption(ComboBoxSdlWaitonerror, null, LabelSdlWaitonerror, globalConf.SDL_waitonerror, userConfData, "sdl", "waitonerror");
                SaveOptionSplitProperty(ComboBoxSdlPriorityActive, ComboBoxSdlPriorityInactive, LabelSdlPriorityActive, LabelSdlPriorityInactive, globalConf.SDL_priority, userConfData, "sdl", "priority");
                SaveOption(ComboBoxSdlMuteWhenInactive, null, LabelSdlMuteWhenInactive, globalConf.SDL_mute_when_inactive, userConfData, "sdl", "mute_when_inactive");
                SaveOption(ComboBoxSdlPauseWhenInactive, null, LabelSdlPauseWhenInactive, globalConf.SDL_pause_when_inactive, userConfData, "sdl", "pause_when_inactive");
                SaveOption(null, TextBoxSdlMapperfile, LabelSdlMapperfile, globalConf.SDL_mapperfile, userConfData, "sdl", "mapperfile");

                // [dosbox]
                SaveOption(ComboBoxDosboxLanguage, null, LabelDosboxLanguage, globalConf.DOSBOX_language, userConfData, "dosbox", "language");
                SaveOption(ComboBoxDosboxMachine, null, LabelDosboxMachine, globalConf.DOSBOX_machine, userConfData, "dosbox", "machine");
                SaveOption(ComboBoxDosboxMemsize, null, LabelDosboxMemsize, globalConf.DOSBOX_memsize, userConfData, "dosbox", "memsize");
                SaveOption(ComboBoxDosboxVmemsize, null, LabelDosboxVmemsize, globalConf.DOSBOX_vmemsize, userConfData, "dosbox", "vmemsize");
                SaveOption(ComboBoxDosboxVmemDelay, TextBoxDosboxVmemDelay, LabelDosboxVmemDelay, globalConf.DOSBOX_vmem_delay, userConfData, "dosbox", "vmem_delay");
                SaveOption(ComboBoxDosboxDosRate, TextBoxDosboxDosRate, LabelDosboxDosRate, globalConf.DOSBOX_dos_rate, userConfData, "dosbox", "dos_rate");
                SaveOption(ComboBoxDosboxMcbFaultStrategy, null, LabelDosboxMcbFaultStrategy, globalConf.DOSBOX_mcb_fault_strategy, userConfData, "dosbox", "mcb_fault_strategy");
                SaveOption(ComboBoxDosboxVesaModes, null, LabelDosboxVesaModes, globalConf.DOSBOX_vesa_modes, userConfData, "dosbox", "vesa_modes");
                SaveOption(ComboBoxDosboxVga8dotFont, null, LabelDosboxVga8dotFont, globalConf.DOSBOX_vga_8dot_font, userConfData, "dosbox", "vga_8dot_font");
                SaveOption(ComboBoxDosboxVgaRenderPerScanline, null, LabelDosboxVgaRenderPerScanline, globalConf.DOSBOX_vga_render_per_scanline, userConfData, "dosbox", "vga_render_per_scanline");
                SaveOption(ComboBoxDosboxSpeedMods, null, LabelDosboxSpeedMods, globalConf.DOSBOX_speed_mods, userConfData, "dosbox", "speed_mods");
                SaveOption(ComboBoxDosboxAutoexecSection, null, LabelDosboxAutoexecSection, globalConf.DOSBOX_autoexec_section, userConfData, "dosbox", "autoexec_section");
                SaveOption(ComboBoxDosboxAutomount, null, LabelDosboxAutomount, globalConf.DOSBOX_automount, userConfData, "dosbox", "automount");
                SaveOption(ComboBoxDosboxStartupVerbosity, null, LabelDosboxStartupVerbosity, globalConf.DOSBOX_startup_verbosity, userConfData, "dosbox", "startup_verbosity");
                SaveOption(ComboBoxDosboxAllowWriteProtectedFiles, null, LabelDosboxAllowWriteProtectedFiles, globalConf.DOSBOX_allow_write_protected_files, userConfData, "dosbox", "allow_write_protected_files");
                SaveOption(ComboBoxDosboxShellConfigShortcuts, null, LabelDosboxShellConfigShortcuts, globalConf.DOSBOX_shell_config_shortcuts, userConfData, "dosbox", "shell_config_shortcuts");

                // [render]
                SaveOption(ComboBoxRenderAspect, null, LabelRenderAspect, globalConf.RENDER_aspect, userConfData, "render", "aspect");
                SaveOption(ComboBoxRenderIntegerScaling, null, LabelRenderIntegerScaling, globalConf.RENDER_integer_scaling, userConfData, "render", "integer_scaling");
                SaveOption(ComboBoxRenderViewport, TextBoxRenderViewport, LabelRenderViewport, globalConf.RENDER_viewport, userConfData, "render", "viewport");
                SaveOption(ComboBoxRenderMonochromePalette, null, LabelRenderMonochromePalette, globalConf.RENDER_monochrome_palette, userConfData, "render", "monochrome_palette");
                SaveOption(ComboBoxRenderCgaColors, TextBoxRenderCgaColors, LabelRenderCgaColors, globalConf.RENDER_cga_colors, userConfData, "render", "cga_colors");
                SaveOption(ComboBoxRenderGlshader, TextBoxRenderGlshader, LabelRenderGlshader, globalConf.RENDER_glshader, userConfData, "render", "glshader");

                // [composite]
                SaveOption(ComboBoxCompositeComposite, null, LabelCompositeComposite, globalConf.COMPOSITE_composite, userConfData, "composite", "composite");
                SaveOption(ComboBoxCompositeEra, null, LabelCompositeEra, globalConf.COMPOSITE_era, userConfData, "composite", "era");
                SaveOption(null, TextBoxCompositeHue, LabelCompositeHue, globalConf.COMPOSITE_hue, userConfData, "composite", "hue");
                SaveOption(null, TextBoxCompositeSaturation, LabelCompositeSaturation, globalConf.COMPOSITE_saturation, userConfData, "composite", "saturation");
                SaveOption(null, TextBoxCompositeContrast, LabelCompositeContrast, globalConf.COMPOSITE_contrast, userConfData, "composite", "contrast");
                SaveOption(null, TextBoxCompositeBrightness, LabelCompositeBrightness, globalConf.COMPOSITE_brightness, userConfData, "composite", "brightness");
                SaveOption(null, TextBoxCompositeConvergence, LabelCompositeConvergence, globalConf.COMPOSITE_convergence, userConfData, "composite", "convergence");

                // [cpu]
                SaveOption(ComboBoxCpuCore, null, LabelCpuCore, globalConf.CPU_core, userConfData, "cpu", "core");
                SaveOption(ComboBoxCpuCputype, null, LabelCpuCputype, globalConf.CPU_cputype, userConfData, "cpu", "cputype");
                SaveOption(ComboBoxCpuCycles, TextBoxCpuCycles, LabelCpuCycles, globalConf.CPU_cycles, userConfData, "cpu", "cycles");
                SaveOption(null, TextBoxCpuCycleup, LabelCpuCycleup, globalConf.CPU_cycleup, userConfData, "cpu", "cycleup");
                SaveOption(null, TextBoxCpuCycledown, LabelCpuCycledown, globalConf.CPU_cycledown, userConfData, "cpu", "cycledown");

                // [Voodoo]
                SaveOption(ComboBoxVoodooVoodoo, null, LabelVoodooVoodoo, globalConf.VOODOO_voodoo, userConfData, "voodoo", "voodoo");
                SaveOption(ComboBoxVoodooVoodooMemsize, null, LabelVoodooVoodooMemsize, globalConf.VOODOO_voodoo_memsize, userConfData, "voodoo", "voodoo_memsize");
                SaveOption(ComboBoxVoodooVoodooMultithreading, null, LabelVoodooVoodooMultithreading, globalConf.VOODOO_voodoo_multithreading, userConfData, "voodoo", "voodoo_multithreading");
                SaveOption(ComboBoxVoodooVoodooBilinearFiltering, null, LabelVoodooVoodooBilinearFiltering, globalConf.VOODOO_voodoo_bilinear_filtering, userConfData, "voodoo", "voodoo_bilinear_filtering");

                // [capture]
                SaveOption(null, TextBoxCaptureCaptureDir, LabelCaptureCaptureDir, globalConf.CAPTURE_capture_dir, userConfData, "capture", "capture_dir");
                SaveOption(ComboBoxCaptureDefaultImageCaptureFormats, null, LabelCaptureDefaultImageCaptureFormats, globalConf.CAPTURE_default_image_capture_formats, userConfData, "capture", "default_image_capture_formats");

                // [mouse]
                SaveOption(ComboBoxMouseMouseCapture, null, LabelMouseMouseCapture, globalConf.MOUSE_mouse_capture, userConfData, "mouse", "mouse_capture");
                SaveOption(ComboBoxMouseMouseMiddleRelease, null, LabelMouseMouseMiddleRelease, globalConf.MOUSE_mouse_middle_release, userConfData, "mouse", "mouse_middle_release");
                SaveOption(ComboBoxMouseMouseMultiDisplayAware, null, LabelMouseMouseMultiDisplayAware, globalConf.MOUSE_mouse_multi_display_aware, userConfData, "mouse", "mouse_multi_display_aware");
                SaveOption(ComboBoxMouseMouseRawInput, null, LabelMouseMouseRawInput, globalConf.MOUSE_mouse_raw_input, userConfData, "mouse", "mouse_raw_input");
                SaveOption(null, TextBoxMouseMouseSensitivity, LabelMouseMouseSensitivity, globalConf.MOUSE_mouse_sensitivity, userConfData, "mouse", "mouse_sensitivity");
                SaveOption(ComboBoxMouseDosMouseDriver, null, LabelMouseDosMouseDriver, globalConf.MOUSE_dos_mouse_driver, userConfData, "mouse", "dos_mouse_driver");
                SaveOption(ComboBoxMouseDosMouseImmediate, null, LabelMouseDosMouseImmediate, globalConf.MOUSE_dos_mouse_immediate, userConfData, "mouse", "dos_mouse_immediate");
                SaveOption(ComboBoxMousePs2MouseModel, null, LabelMousePs2MouseModel, globalConf.MOUSE_ps2_mouse_model, userConfData, "mouse", "ps2_mouse_model");
                SaveOption(ComboBoxMouseComMouseModel, null, LabelMouseComMouseModel, globalConf.MOUSE_com_mouse_model, userConfData, "mouse", "com_mouse_model");
                SaveOption(ComboBoxMouseVmwareMouse, null, LabelMouseVmwareMouse, globalConf.MOUSE_vmware_mouse, userConfData, "mouse", "vmware_mouse");
                SaveOption(ComboBoxMouseVirtualboxMouse, null, LabelMouseVirtualboxMouse, globalConf.MOUSE_virtualbox_mouse, userConfData, "mouse", "virtualbox_mouse");

                // [mixer]
                SaveOption(ComboBoxMixerNosound, null, LabelMixerNosound, globalConf.MIXER_nosound, userConfData, "mixer", "nosound");
                SaveOption(ComboBoxMixerRate, null, LabelMixerRate, globalConf.MIXER_rate, userConfData, "mixer", "rate");
                SaveOption(ComboBoxMixerBlocksize, null, LabelMixerBlocksize, globalConf.MIXER_blocksize, userConfData, "mixer", "blocksize");
                SaveOption(null, TextBoxMixerPrebuffer, LabelMixerPrebuffer, globalConf.MIXER_prebuffer, userConfData, "mixer", "prebuffer");
                SaveOption(ComboBoxMixerNegotiate, null, LabelMixerNegotiate, globalConf.MIXER_negotiate, userConfData, "mixer", "negotiate");
                SaveOption(ComboBoxMixerCompressor, null, LabelMixerCompressor, globalConf.MIXER_compressor, userConfData, "mixer", "compressor");
                SaveOption(ComboBoxMixerCrossfeed, null, LabelMixerCrossfeed, globalConf.MIXER_crossfeed, userConfData, "mixer", "crossfeed");
                SaveOption(ComboBoxMixerReverb, null, LabelMixerReverb, globalConf.MIXER_reverb, userConfData, "mixer", "reverb");
                SaveOption(ComboBoxMixerChorus, null, LabelMixerChorus, globalConf.MIXER_chorus, userConfData, "mixer", "chorus");

                // [midi]
                SaveOption(ComboBoxMidiMididevice, null, LabelMidiMididevice, globalConf.MIDI_mididevice, userConfData, "midi", "mididevice");
                SaveOption(null, TextBoxMidiMidiconfig, LabelMidiMidiconfig, globalConf.MIDI_midiconfig, userConfData, "midi", "midiconfig");
                SaveOption(ComboBoxMidiMpu401, null, LabelMidiMpu401, globalConf.MIDI_mpu401, userConfData, "midi", "mpu401");
                SaveOption(ComboBoxMidiRawMidiOutput, null, LabelMidiRawMidiOutput, globalConf.MIDI_raw_midi_output, userConfData, "midi", "raw_midi_output");

                // [fluidsynth]
                SaveOption(null, TextBoxFluidsynthSoundfont, LabelFluidsynthSoundfont, globalConf.FLUIDSYNTH_soundfont, userConfData, "fluidsynth", "soundfont");
                SaveOption(ComboBoxFluidsynthFsynthChorus, TextBoxFluidsynthFsynthChorus, LabelFluidsynthFsynthChorus, globalConf.FLUIDSYNTH_fsynth_chorus, userConfData, "fluidsynth", "fsynth_chorus");
                SaveOption(ComboBoxFluidsynthFsynthReverb, TextBoxFluidsynthFsynthReverb, LabelFluidsynthFsynthReverb, globalConf.FLUIDSYNTH_fsynth_reverb, userConfData, "fluidsynth", "fsynth_reverb");
                SaveOption(ComboBoxFluidsynthFsynthFilter, TextBoxFluidsynthFsynthFilter, LabelFluidsynthFsynthFilter, globalConf.FLUIDSYNTH_fsynth_filter, userConfData, "fluidsynth", "fsynth_filter");

                // [mt32]
                SaveOption(ComboBoxMt32Model, null, LabelMt32Model, globalConf.MT32_model, userConfData, "mt32", "model");
                SaveOption(null, TextBoxMt32Romdir, LabelMt32Romdir, globalConf.MT32_romdir, userConfData, "mt32", "romdir");
                SaveOption(ComboBoxMt32Mt32Filter, TextBoxMt32Mt32Filter, LabelMt32Mt32Filter, globalConf.MT32_mt32_filter, userConfData, "mt32", "mt32_filter");

                // [sblaster]
                SaveOption(ComboBoxSblasterSbtype, null, LabelSblasterSbtype, globalConf.SBLASTER_sbtype, userConfData, "sblaster", "sbtype");
                SaveOption(ComboBoxSblasterSbbase, null, LabelSblasterSbbase, globalConf.SBLASTER_sbbase, userConfData, "sblaster", "sbbase");
                SaveOption(ComboBoxSblasterIrq, null, LabelSblasterIrq, globalConf.SBLASTER_irq, userConfData, "sblaster", "irq");
                SaveOption(ComboBoxSblasterDma, null, LabelSblasterDma, globalConf.SBLASTER_dma, userConfData, "sblaster", "dma");
                SaveOption(ComboBoxSblasterHdma, null, LabelSblasterHdma, globalConf.SBLASTER_hdma, userConfData, "sblaster", "hdma");
                SaveOption(ComboBoxSblasterSbmixer, null, LabelSblasterSbmixer, globalConf.SBLASTER_sbmixer, userConfData, "sblaster", "sbmixer");
                SaveOption(null, TextBoxSblasterSbwarmup, LabelSblasterSbwarmup, globalConf.SBLASTER_sbwarmup, userConfData, "sblaster", "sbwarmup");
                SaveOption(ComboBoxSblasterOplmode, null, LabelSblasterOplmode, globalConf.SBLASTER_oplmode, userConfData, "sblaster", "oplmode");
                SaveOption(ComboBoxSblasterOplFadeout, TextBoxSblasterOplFadeout, LabelSblasterOplFadeout, globalConf.SBLASTER_opl_fadeout, userConfData, "sblaster", "opl_fadeout");
                SaveOption(ComboBoxSblasterSbFilter, TextBoxSblasterSbFilter, LabelSblasterSbFilter, globalConf.SBLASTER_sb_filter, userConfData, "sblaster", "sb_filter");
                SaveOption(ComboBoxSblasterSbFilterAlwaysOn, null, LabelSblasterSbFilterAlwaysOn, globalConf.SBLASTER_sb_filter_always_on, userConfData, "sblaster", "sb_filter_always_on");
                SaveOption(ComboBoxSblasterOplFilter, TextBoxSblasterOplFilter, LabelSblasterOplFilter, globalConf.SBLASTER_opl_filter, userConfData, "sblaster", "opl_filter");
                SaveOption(ComboBoxSblasterCmsFilter, TextBoxSblasterCmsFilter, LabelSblasterCmsFilter, globalConf.SBLASTER_cms_filter, userConfData, "sblaster", "cms_filter");

                // [gus]
                SaveOption(ComboBoxGusGus, null, LabelGusGus, globalConf.GUS_gus, userConfData, "gus", "gus");
                SaveOption(ComboBoxGusGusbase, null, LabelGusGusbase, globalConf.GUS_gusbase, userConfData, "gus", "gusbase");
                SaveOption(ComboBoxGusGusirq, null, LabelGusGusirq, globalConf.GUS_gusirq, userConfData, "gus", "gusirq");
                SaveOption(ComboBoxGusGusdma, null, LabelGusGusdma, globalConf.GUS_gusdma, userConfData, "gus", "gusdma");
                SaveOption(null, TextBoxGusUltradir, LabelGusUltradir, globalConf.GUS_ultradir, userConfData, "gus", "ultradir");
                SaveOption(ComboBoxGusGusFilter, TextBoxGusGusFilter, LabelGusGusFilter, globalConf.GUS_gus_filter, userConfData, "gus", "gus_filter");

                // [imfc]
                SaveOption(ComboBoxImfcImfc, null, LabelImfcImfc, globalConf.IMFC_imfc, userConfData, "imfc", "imfc");
                SaveOption(ComboBoxImfcImfcBase, null, LabelImfcImfcBase, globalConf.IMFC_imfc_base, userConfData, "imfc", "imfc_base");
                SaveOption(ComboBoxImfcImfcIrq, null, LabelImfcImfcIrq, globalConf.IMFC_imfc_irq, userConfData, "imfc", "imfc_irq");
                SaveOption(ComboBoxImfcImfcFilter, TextBoxImfcImfcFilter, LabelImfcImfcFilter, globalConf.IMFC_imfc_filter, userConfData, "imfc", "imfc_filter");

                // [innovation]
                SaveOption(ComboBoxInnovationSidmodel, null, LabelInnovationSidmodel, globalConf.INNOVATION_sidmodel, userConfData, "innovation", "sidmodel");
                SaveOption(ComboBoxInnovationSidclock, null, LabelInnovationSidclock, globalConf.INNOVATION_sidclock, userConfData, "innovation", "sidclock");
                SaveOption(ComboBoxInnovationSidport, null, LabelInnovationSidport, globalConf.INNOVATION_sidport, userConfData, "innovation", "sidport");
                SaveOption(null, TextBoxInnovation6581filter, LabelInnovation6581filter, globalConf.INNOVATION_6581filter, userConfData, "innovation", "6581filter");
                SaveOption(null, TextBoxInnovation8580filter, LabelInnovation8580filter, globalConf.INNOVATION_8580filter, userConfData, "innovation", "8580filter");
                SaveOption(ComboBoxInnovationInnovationFilter, TextBoxInnovationInnovationFilter, LabelInnovationInnovationFilter, globalConf.INNOVATION_innovation_filter, userConfData, "innovation", "innovation_filter");

                // [speaker]
                SaveOption(ComboBoxSpeakerPcspeaker, null, LabelSpeakerPcspeaker, globalConf.SPEAKER_pcspeaker, userConfData, "speaker", "pcspeaker");
                SaveOption(ComboBoxSpeakerPcspeakerFilter, TextBoxSpeakerPcspeakerFilter, LabelSpeakerPcspeakerFilter, globalConf.SPEAKER_pcspeaker_filter, userConfData, "speaker", "pcspeaker_filter");
                SaveOption(ComboBoxSpeakerTandy, null, LabelSpeakerTandy, globalConf.SPEAKER_tandy, userConfData, "speaker", "tandy");
                SaveOption(ComboBoxSpeakerTandyFadeout, TextBoxSpeakerTandyFadeout, LabelSpeakerTandyFadeout, globalConf.SPEAKER_tandy_fadeout, userConfData, "speaker", "tandy_fadeout");
                SaveOption(ComboBoxSpeakerTandyFilter, TextBoxSpeakerTandyFilter, LabelSpeakerTandyFilter, globalConf.SPEAKER_tandy_filter, userConfData, "speaker", "tandy_filter");
                SaveOption(ComboBoxSpeakerTandyDacFilter, TextBoxSpeakerTandyDacFilter, LabelSpeakerTandyDacFilter, globalConf.SPEAKER_tandy_dac_filter, userConfData, "speaker", "tandy_dac_filter");
                SaveOption(ComboBoxSpeakerLptDac, null, LabelSpeakerLptDac, globalConf.SPEAKER_lpt_dac, userConfData, "speaker", "lpt_dac");
                SaveOption(ComboBoxSpeakerLptDacFilter, TextBoxSpeakerLptDacFilter, LabelSpeakerLptDacFilter, globalConf.SPEAKER_lpt_dac_filter, userConfData, "speaker", "lpt_dac_filter");
                SaveOption(ComboBoxSpeakerPs1audio, null, LabelSpeakerPs1audio, globalConf.SPEAKER_ps1audio, userConfData, "speaker", "ps1audio");
                SaveOption(ComboBoxSpeakerPs1audioFilter, TextBoxSpeakerPs1audioFilter, LabelSpeakerPs1audioFilter, globalConf.SPEAKER_ps1audio_filter, userConfData, "speaker", "ps1audio_filter");
                SaveOption(ComboBoxSpeakerPs1audioDacFilter, TextBoxSpeakerPs1audioDacFilter, LabelSpeakerPs1audioDacFilter, globalConf.SPEAKER_ps1audio_dac_filter, userConfData, "speaker", "ps1audio_dac_filter");

                // [reelmagic]
                SaveOption(ComboBoxReelmagicReelmagic, null, LabelReelmagicReelmagic, globalConf.REELMAGIC_reelmagic, userConfData, "reelmagic", "reelmagic");
                SaveOption(ComboBoxReelmagicReelmagicKey, TextBoxReelmagicReelmagicKey, LabelReelmagicReelmagicKey, globalConf.REELMAGIC_reelmagic_key, userConfData, "reelmagic", "reelmagic_key");
                SaveOption(ComboBoxReelmagicReelmagicFcode, null, LabelReelmagicReelmagicFcode, globalConf.REELMAGIC_reelmagic_fcode, userConfData, "reelmagic", "reelmagic_fcode");

                // [joystick]
                SaveOption(ComboBoxJoystickJoysticktype, null, LabelJoystickJoysticktype, globalConf.JOYSTICK_joysticktype, userConfData, "joystick", "joysticktype");
                SaveOption(ComboBoxJoystickTimed, null, LabelJoystickTimed, globalConf.JOYSTICK_timed, userConfData, "joystick", "timed");
                SaveOption(ComboBoxJoystickAutofire, null, LabelJoystickAutofire, globalConf.JOYSTICK_autofire, userConfData, "joystick", "autofire");
                SaveOption(ComboBoxJoystickSwap34, null, LabelJoystickSwap34, globalConf.JOYSTICK_swap34, userConfData, "joystick", "swap34");
                SaveOption(ComboBoxJoystickButtonwrap, null, LabelJoystickButtonwrap, globalConf.JOYSTICK_buttonwrap, userConfData, "joystick", "buttonwrap");
                SaveOption(ComboBoxJoystickCircularinput, null, LabelJoystickCircularinput, globalConf.JOYSTICK_circularinput, userConfData, "joystick", "circularinput");
                SaveOption(null, TextBoxJoystickDeadzone, LabelJoystickDeadzone, globalConf.JOYSTICK_deadzone, userConfData, "joystick", "deadzone");
                SaveOption(ComboBoxJoystickUseJoyCalibrationHotkeys, null, LabelJoystickUseJoyCalibrationHotkeys, globalConf.JOYSTICK_use_joy_calibration_hotkeys, userConfData, "joystick", "use_joy_calibration_hotkeys");
                SaveOption(ComboBoxJoystickJoyXCalibration, null, LabelJoystickJoyXCalibration, globalConf.JOYSTICK_joy_x_calibration, userConfData, "joystick", "joy_x_calibration");
                SaveOption(ComboBoxJoystickJoyYCalibration, null, LabelJoystickJoyYCalibration, globalConf.JOYSTICK_joy_y_calibration, userConfData, "joystick", "joy_y_calibration");

                // [serial]
                SaveOption(ComboBoxSerialSerial1, TextBoxSerialSerial1, LabelSerialSerial1, globalConf.SERIAL_serial1, userConfData, "serial", "serial1");
                SaveOption(ComboBoxSerialSerial2, TextBoxSerialSerial2, LabelSerialSerial2, globalConf.SERIAL_serial2, userConfData, "serial", "serial2");
                SaveOption(ComboBoxSerialSerial3, TextBoxSerialSerial3, LabelSerialSerial3, globalConf.SERIAL_serial3, userConfData, "serial", "serial3");
                SaveOption(ComboBoxSerialSerial4, TextBoxSerialSerial4, LabelSerialSerial4, globalConf.SERIAL_serial4, userConfData, "serial", "serial4");
                SaveOption(null, TextBoxSerialPhonebookfile, LabelSerialPhonebookfile, globalConf.SERIAL_phonebookfile, userConfData, "serial", "phonebookfile");

                // [dos]
                SaveOption(ComboBoxDosXms, null, LabelDosXms, globalConf.DOS_xms, userConfData, "dos", "xms");
                SaveOption(ComboBoxDosEms, null, LabelDosEms, globalConf.DOS_ems, userConfData, "dos", "ems");
                SaveOption(ComboBoxDosUmb, null, LabelDosUmb, globalConf.DOS_umb, userConfData, "dos", "umb");
                SaveOption(ComboBoxDosVer, TextBoxDosVer, LabelDosVer, globalConf.DOS_ver, userConfData, "dos", "ver");
                SaveOption(ComboBoxDosLocalePeriod, null, LabelDosLocalePeriod, globalConf.DOS_locale_period, userConfData, "dos", "locale_period");
                SaveOption(ComboBoxDosCountry, TextBoxDosCountry, LabelDosCountry, globalConf.DOS_country, userConfData, "dos", "country");
                SaveOption(ComboBoxDosKeyboardlayout, TextBoxDosKeyboardlayout, LabelDosKeyboardlayout, globalConf.DOS_keyboardlayout, userConfData, "dos", "keyboardlayout");
                SaveOption(ComboBoxDosExpandShellVariable, null, LabelDosExpandShellVariable, globalConf.DOS_expand_shell_variable, userConfData, "dos", "expand_shell_variable");
                SaveOption(null, TextBoxDosShellHistoryFile, LabelDosShellHistoryFile, globalConf.DOS_shell_history_file, userConfData, "dos", "shell_history_file");
                SaveOption(null, TextBoxDosSetverTableFile, LabelDosSetverTableFile, globalConf.DOS_setver_table_file, userConfData, "dos", "setver_table_file");
                SaveOption(ComboBoxDosPcjrMemoryConfig, null, LabelDosPcjrMemoryConfig, globalConf.DOS_pcjr_memory_config, userConfData, "dos", "pcjr_memory_config");

                // [ipx]
                SaveOption(ComboBoxIpxIpx, null, LabelIpxIpx, globalConf.IPX_ipx, userConfData, "ipx", "ipx");

                // [ethernet]
                SaveOption(ComboBoxEthernetNe2000, null, LabelEthernetNe2000, globalConf.ETHERNET_ne2000, userConfData, "ethernet", "ne2000");
                SaveOption(ComboBoxEthernetNicbase, null, LabelEthernetNicbase, globalConf.ETHERNET_nicbase, userConfData, "ethernet", "nicbase");
                SaveOption(ComboBoxEthernetNicirq, null, LabelEthernetNicirq, globalConf.ETHERNET_nicirq, userConfData, "ethernet", "nicirq");
                SaveOption(null, TextBoxEthernetMacaddr, LabelEthernetMacaddr, globalConf.ETHERNET_macaddr, userConfData, "ethernet", "macaddr");
                SaveOption(null, TextBoxEthernetTcpPortForwards, LabelEthernetTcpPortForwards, globalConf.ETHERNET_tcp_port_forwards, userConfData, "ethernet", "tcp_port_forwards");
                SaveOption(null, TextBoxEthernetUdpPortForwards, LabelEthernetUdpPortForwards, globalConf.ETHERNET_udp_port_forwards, userConfData, "ethernet", "udp_port_forwards");

                // [autoexec]
                // It is neccecary to handle this section manually
                //SaveOption(null, TextBoxAutoexec, LabelAutoexec, globalConf.AUTOEXEC_autoexec, userConfData, "autoexec", "autoexec");

                // Save data to the user.conf file
                parser.WriteFile(userConfFilePath, userConfData);
            }
        }

        private void SaveAutoexecSection(string userConfFileName)
        {
            // The autoexec section is a special case because it is a multiline TextBox

            // Only save the autoexec section if the textbox has anything written in it
            if (string.IsNullOrEmpty(TextBoxAutoexec.Text.Trim()))
            {
                return;
            }

            string userConfFilePath = Path.Combine(Properties.Settings.Default.UserConfFolderPath, userConfFileName);

            // First, open the user configuration file
            if (userConfFilePath == null || !File.Exists(userConfFilePath))
            {
                return;
            }

            // Read the user configuration file and find if the autoexec section is present
            string[] lines = File.ReadAllLines(userConfFilePath);
            List<string> newLines = new();

            bool autoexecSectionFound = false;
            bool autoexecSectionStarted = false;
            bool autoexecSectionEnded = false;

            foreach (string line in lines)
            {
                if (line == "[autoexec]")
                {
                    autoexecSectionFound = true;
                    autoexecSectionStarted = true;
                    newLines.Add(line);
                    continue;
                }

                if (autoexecSectionStarted && !autoexecSectionEnded)
                {
                    if (line == "")
                    {
                        autoexecSectionEnded = true;
                    }
                    else
                    {
                        newLines.Add(line);
                    }
                }
                else
                {
                    newLines.Add(line);
                }
            }

            // Add the new autoexec section
            if (!autoexecSectionFound)
            {
                newLines.Add("");
                newLines.Add("[autoexec]");
            }
            newLines.Add(TextBoxAutoexec.Text.Trim());

            // Write the new user configuration file
            File.WriteAllLines(userConfFilePath, newLines);
        }

        private bool TextBoxHasErrors()
        {
            // Check if the mandatory textboxes are empty when they are enabled
            foreach (TabPage tabPage in TabControlOptions.TabPages)
            {
                foreach (Control control in tabPage.Controls)
                {
                    if (control is TextBox textBox && textBox.Enabled && !textBox.ReadOnly && textBox != TextBoxMidiMidiconfig &&
                        textBox != TextBoxEthernetTcpPortForwards && textBox != TextBoxEthernetUdpPortForwards && textBox != TextBoxAutoexec)
                    {
                        if (string.IsNullOrEmpty(textBox.Text))
                        {
                            string labelText = GetLabelTextFromTextBox(textBox);
                            MessageBox.Show("The field " + labelText + " can't be empty.", "Data entry error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox.Focus();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private string GetLabelTextFromTextBox(TextBox textBox)
        {
            // This function gets the text of the label associated with a TextBox

            string labelName = "Label" + textBox.Name.Substring(7); // Get the name of the Label associated with the TextBox
            Label? label = Controls.Find(labelName, true).FirstOrDefault() as Label; // Search for the Label in the form

            if (label != null)
            {
                return label.Text;
            }
            return string.Empty; // if the label is not found return an empty string
        }

        private static void OpenForm(Form form, bool IsDialog)
        {
            // Open the form with the given name
            if (IsDialog)
            {
                form.ShowDialog();
            }
            else
            {
                form.Show();
            }
        }

        public void LoadAllUserConfs()
        {
            // If paths are not set, return
            if (!PathsAreSet())
            {
                return;
            }

            // Check if the user confs folder exists
            if (Directory.Exists(Properties.Settings.Default.UserConfFolderPath))
            {
                // Load all the user confs present in the user confs folder and add them to the ListBox
                string userConfsFolder = Properties.Settings.Default.UserConfFolderPath;
                string[] userConfs = Directory.GetFiles(userConfsFolder, "*.conf");

                foreach (string userConf in userConfs)
                {
                    string fileName = Path.GetFileName(userConf);
                    ListBoxUserConfs.Items.Add(fileName);
                }

                // Update the original items in the ListBox
                listBoxItems = ListBoxUserConfs.Items.Cast<string>().ToList();
            }
        }

        private static void InsertTextIntoTextBox(string textToInsert, TextBox textBox)
        {
            // This function inserts text into a TextBox at the current cursor position

            // Get the current cursor position
            int insertPos = textBox.SelectionStart;

            textBox.Text = textBox.Text.Insert(insertPos, textToInsert);

            // Place the cursor after the inserted text
            textBox.SelectionStart = insertPos + textToInsert.Length;
        }

        private void ToolStripMenuItemInsertFilePath_Click(object sender, EventArgs e)
        {
            // Open a file dialog to select a file
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.Title = "Select a file to insert";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                InsertTextIntoTextBox(filePath, TextBoxAutoexec);
            }
        }

        private void ToolStripMenuItemInsertFolderPath_Click(object sender, EventArgs e)
        {
            // Open a folder dialog to select a folder
            FolderBrowserDialog folderBrowserDialog = new();
            folderBrowserDialog.Description = "Select a folder to insert";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderBrowserDialog.SelectedPath;
                InsertTextIntoTextBox(folderPath, TextBoxAutoexec);
            }
        }

        private void MountDrive(string driveLetter, string? asDriveType)
        {
            // Open a folder dialog to select a folder
            FolderBrowserDialog folderBrowserDialog = new();
            folderBrowserDialog.Description = "Select a folder to mount";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderBrowserDialog.SelectedPath;
                InsertTextIntoTextBox("mount " + driveLetter + " \"" + folderPath + "\"" + asDriveType, TextBoxAutoexec);
            }
        }

        private void ImgmountDrive(string driveLetter, string? asDriveType)
        {
            // Open a file dialog to select a file
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.Title = "Select a file to mount";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                InsertTextIntoTextBox("imgmount " + driveLetter + " " + "\"" + filePath + "\"" + asDriveType, TextBoxAutoexec);
            }
        }

        private void ToolStripMenuItemInsertCALL_Click(object sender, EventArgs e)
        {
            InsertTextIntoTextBox("CALL ", TextBoxAutoexec);
        }

        private void ToolStripMenuItemInsertCD_Click(object sender, EventArgs e)
        {
            InsertTextIntoTextBox("CD ", TextBoxAutoexec);
        }

        private void ToolStripMenuItemInsertCLS_Click(object sender, EventArgs e)
        {
            InsertTextIntoTextBox("CLS", TextBoxAutoexec);
        }

        private void ToolStripMenuItemInsertDOSKEY_Click(object sender, EventArgs e)
        {
            InsertTextIntoTextBox("DOSKEY", TextBoxAutoexec);
        }

        private void ToolStripMenuItemInsertECHO_Click(object sender, EventArgs e)
        {
            InsertTextIntoTextBox("ECHO ", TextBoxAutoexec);
        }

        private void ToolStripMenuItemInsertEXIT_Click(object sender, EventArgs e)
        {
            InsertTextIntoTextBox("EXIT", TextBoxAutoexec);
        }

        private void ToolStripMenuItemInsertMountAsHDD_Click(object sender, EventArgs e)
        {
            MountDrive("C", null);
        }

        private void ToolStripMenuItemInsertMountAsFDD_Click(object sender, EventArgs e)
        {
            MountDrive("A", " -t floppy");
        }

        private void ToolStripMenuItemInsertImgmountAsCDROM_Click(object sender, EventArgs e)
        {
            ImgmountDrive("D", " -t cdrom");
        }

        private void ToolStripMenuItemInsertImgmountAsFDD_Click(object sender, EventArgs e)
        {
            ImgmountDrive("A", " -t floppy");
        }

        private void ToolStripMenuItemInsertImgmountAsHDD_Click(object sender, EventArgs e)
        {
            ImgmountDrive("C", " -size 512,63,16,X");
        }

        private void ToolStripButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ToolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ToolStripMenuItemOptions_Click(object sender, EventArgs e)
        {
            OpenForm(new FormOptions(), true);
        }

        private void ToolStripButtonOptions_Click(object sender, EventArgs e)
        {
            OpenForm(new FormOptions(), true);
        }

        private void ToolStripButtonNew_Click(object sender, EventArgs e)
        {
            try
            {
                NewUserConf();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ToolStripMenuItemNew_Click(object sender, EventArgs e)
        {
            try
            {
                NewUserConf();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ToolStripButtonSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ToolStripMenuItemSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ToolStripButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ToolStripButtonLaunch_Click(object sender, EventArgs e)
        {
            try
            {
                Launch(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static string SelectFolderRelativeToDosBox() // Returns the path relative to the dosbox folder
        {
            if (PathsAreSet() == false)
            {
                return string.Empty;
            }

            // Get the dosbox folder path from the DosBox-Staging exe file path
            string? dosBoxFolderPath = Path.GetDirectoryName(Properties.Settings.Default.DosBoxStagingExeFilePath);

            if (dosBoxFolderPath == null)
            {
                return string.Empty;
            }

            // Open a folder dialog to select a folder
            FolderBrowserDialog folderBrowserDialog = new();
            folderBrowserDialog.Description = "Select a folder";
            folderBrowserDialog.InitialDirectory = dosBoxFolderPath;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderBrowserDialog.SelectedPath;
                // If the selected folder is a subfolder of the dosbox folder, return the path relative to the dosbox folder
                if (folderPath.StartsWith(dosBoxFolderPath))
                {
                    return folderPath.Substring(dosBoxFolderPath.Length + 1); // Do not show the first '\'
                }
                else
                {
                    throw new Exception("The selected folder is not a subfolder of the DosBox-Staging folder.");
                }
            }
            return string.Empty;
        }

        private static string SelectFileRelativeTo(string path, string filter, string title) // Returns the relative path to the path of the function
        {
            // If the path does not exist, return an empty string
            if (!Directory.Exists(path))
            {
                throw new Exception("The path does not exist:\n\n" + path);
            }

            // Open a file dialog to select a file
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = filter;
            openFileDialog.Title = title;
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = path;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                // If the selected file is in the dosbox folder, return the path relative to the dosbox folder
                if (filePath.StartsWith(path))
                {
                    return filePath.Substring(path.Length + 1); // Do not show the first '\'
                }
                else
                {
                    throw new Exception("The selected file must be in a subfolder of \n\n" + path);
                }
            }
            return string.Empty;
        }

        private void ButtonCaptureCaptureDir_Click(object sender, EventArgs e)
        {
            try
            {
                string captureDir = SelectFolderRelativeToDosBox();
                if (!string.IsNullOrEmpty(captureDir))
                {
                    TextBoxCaptureCaptureDir.Text = captureDir;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonMt32Romdir_Click(object sender, EventArgs e)
        {
            try
            {
                string mt32Romdir = SelectFolderRelativeToDosBox();
                if (!string.IsNullOrEmpty(mt32Romdir))
                {
                    TextBoxMt32Romdir.Text = mt32Romdir;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonSdlMapperfile_Click(object sender, EventArgs e)
        {
            // Select a mapper file relative to the '\resources\mapperfiles' subfolder of the dosbox folder
            try
            {
                if (!PathsAreSet())
                {
                    return;
                }

                // Get the dosbox folder path from the DosBox-Staging exe file path
                string? dosBoxFolderPath = Path.GetDirectoryName(Properties.Settings.Default.DosBoxStagingExeFilePath);

                if (dosBoxFolderPath == null)
                {
                    return;
                }

                // The mapper files are located in the '\resources\mapperfiles' subfolder of the dosbox folder
                string path = Path.Combine(dosBoxFolderPath, "resources\\mapperfiles");

                string mapperfile = SelectFileRelativeTo(path, "Mapper files (*.map)|*.map", "Select a mapper file");
                if (!string.IsNullOrEmpty(mapperfile))
                {
                    TextBoxSdlMapperfile.Text = mapperfile;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonFluidsynthSoundfont_Click(object sender, EventArgs e)
        {
            // Select a soundfont file relative to the '\soundfonts' subfolder of the dosbox folder
            try
            {
                if (!PathsAreSet())
                {
                    return;
                }

                // Get the dosbox folder path from the DosBox-Staging exe file path
                string? dosBoxFolderPath = Path.GetDirectoryName(Properties.Settings.Default.DosBoxStagingExeFilePath);

                if (dosBoxFolderPath == null)
                {
                    return;
                }

                // The sf2 files are located in the '\soundfonts' subfolder of the dosbox folder
                string path = Path.Combine(dosBoxFolderPath, "soundfonts");

                string soundfont = SelectFileRelativeTo(path, "Soundfont files (*.sf2)|*.sf2", "Select a soundfont file");
                if (!string.IsNullOrEmpty(soundfont))
                {
                    TextBoxFluidsynthSoundfont.Text = soundfont;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonSerialPhonebookfile_Click(object sender, EventArgs e)
        {
            // Select a phonebook file relative to the dosbox folder
            try
            {
                if (!PathsAreSet())
                {
                    return;
                }

                // Get the dosbox folder path from the DosBox-Staging exe file path
                string? dosBoxFolderPath = Path.GetDirectoryName(Properties.Settings.Default.DosBoxStagingExeFilePath);

                if (dosBoxFolderPath == null)
                {
                    return;
                }

                string phonebook = SelectFileRelativeTo(dosBoxFolderPath, "Phonebook files (*.txt)|*.txt", "Select a phonebook file");
                if (!string.IsNullOrEmpty(phonebook))
                {
                    TextBoxSerialPhonebookfile.Text = phonebook;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonDosShellHistoryFile_Click(object sender, EventArgs e)
        {
            // Select a shell_history file relative to the dosbox folder
            try
            {
                if (!PathsAreSet())
                {
                    return;
                }

                // Get the dosbox folder path from the DosBox-Staging exe file path
                string? dosBoxFolderPath = Path.GetDirectoryName(Properties.Settings.Default.DosBoxStagingExeFilePath);

                if (dosBoxFolderPath == null)
                {
                    return;
                }

                string shellHistory = SelectFileRelativeTo(dosBoxFolderPath, "Shell History files (*.txt)|*.txt", "Select a shell history file");
                if (!string.IsNullOrEmpty(shellHistory))
                {
                    TextBoxDosShellHistoryFile.Text = shellHistory;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonDosSetverTableFile_Click(object sender, EventArgs e)
        {
            // Select a setver_table_file file relative to the dosbox folder
            try
            {
                if (!PathsAreSet())
                {
                    return;
                }

                // Get the dosbox folder path from the DosBox-Staging exe file path
                string? dosBoxFolderPath = Path.GetDirectoryName(Properties.Settings.Default.DosBoxStagingExeFilePath);

                if (dosBoxFolderPath == null)
                {
                    return;
                }

                string setvertable = SelectFileRelativeTo(dosBoxFolderPath, "Setver table files (*.txt)|*.txt", "Select a setver table file");
                if (!string.IsNullOrEmpty(setvertable))
                {
                    TextBoxDosSetverTableFile.Text = setvertable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static string GetValueFromGlobalConf(string key)
        {
            // This function returns the value of any key from the global configuration

            string globalConfFilePath = Properties.Settings.Default.GlobalConfFilePath;
            GlobalConf globalConf = GetGlobalConf(globalConfFilePath);

            // Return the value of any key from the global configuration
            return globalConf.GetType().GetProperty(key)?.GetValue(globalConf, null)?.ToString() ?? string.Empty;
        }

        private void ButtonSdlMapperfileReset_Click(object sender, EventArgs e)
        {
            // Get the default value from the global configuration
            try
            {
                TextBoxSdlMapperfile.Text = GetValueFromGlobalConf("SDL_mapperfile");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonCaptureCaptureDirReset_Click(object sender, EventArgs e)
        {
            // Get the default value from the global configuration
            try
            {
                TextBoxCaptureCaptureDir.Text = GetValueFromGlobalConf("CAPTURE_capture_dir");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonFluidsynthSoundfontReset_Click(object sender, EventArgs e)
        {
            // Get the default value from the global configuration
            try
            {
                TextBoxFluidsynthSoundfont.Text = GetValueFromGlobalConf("FLUIDSYNTH_soundfont");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonMt32RomdirReset_Click(object sender, EventArgs e)
        {
            // Get the default value from the global configuration
            try
            {
                TextBoxMt32Romdir.Text = GetValueFromGlobalConf("MT32_romdir");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonSerialPhonebookfileReset_Click(object sender, EventArgs e)
        {
            // Get the default value from the global configuration
            try
            {
                TextBoxSerialPhonebookfile.Text = GetValueFromGlobalConf("SERIAL_phonebookfile");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonDosShellHistoryFileReset_Click(object sender, EventArgs e)
        {
            // Get the default value from the global configuration
            try
            {
                TextBoxDosShellHistoryFile.Text = GetValueFromGlobalConf("DOS_shell_history_file");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonDosSetverTableFileReset_Click(object sender, EventArgs e)
        {
            // Get the default value from the global configuration
            try
            {
                TextBoxDosSetverTableFile.Text = GetValueFromGlobalConf("DOS_setver_table_file");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ToolStripButtonAbout_Click(object sender, EventArgs e)
        {
            // Open the about form
            OpenForm(new FormAbout(), true);
        }

        private void ToolStripButtonHelp_Click(object sender, EventArgs e)
        {
            // Open the official help form
            OpenForm(new FormHelp(), false);
        }

        private void ToolStripButtonLaunchWithParameters_Click(object sender, EventArgs e)
        {
            // Launch with parameters
            try
            {
                string parameters = Settings.Default.LaunchParameters;
                if (!string.IsNullOrEmpty(parameters))
                {
                    Launch(parameters);
                }
                else
                {
                    throw new Exception("Launch parameters not found. \n\nPlease, check if the parameter section is set in the options.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ListBoxUserConfs_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            // Define the color from argb values
            Color customColor = GlobalSettings.ColorHighlight;
            if (e.Font == null) return;

            // Draw the background of the item. Use a custom color for the selected state.
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                // Increase the height of the rectangle by N pixels
                int numberOfPixels = 0;

                Rectangle rect = new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height + numberOfPixels);

                e.Graphics.FillRectangle(new SolidBrush(customColor), rect); // Custom color for the selected item
                e.Graphics.DrawString(ListBoxUserConfs.Items[e.Index].ToString(), e.Font, Brushes.White, rect); // White text for the selected item
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(ListBoxUserConfs.BackColor), e.Bounds); // Default background color
                e.Graphics.DrawString(ListBoxUserConfs.Items[e.Index].ToString(), e.Font, new SolidBrush(ListBoxUserConfs.ForeColor), e.Bounds); // Default text color for other items
            }

            // Draw the border of the selected item (optional)
            e.DrawFocusRectangle();
        }


        /*
        private bool TextBoxHasErrors()
        {
            // Regular expressions to validate the input
            Regex regexResolution = new(@"^([1-9]\d*)x([1-9]\d*)$"); // No decimals allowed
            Regex regexPosition = new(@"^([1-9]\d*)x([1-9]\d*)$"); // No decimals allowed
            Regex regexNumberGreaterEqual23 = new(@"^(2[3-9]|[3-9]\d+)(\.\d{1,3})?$"); // Decimals allowed
            Regex regexNumberGreaterEqual0 = new Regex(@"^0$|^[1-9]\d*$"); // No decimals allowed

            // [sdl]
            TextBox textBox = TextBoxSdlFullresolution;
            if (textBox.Enabled)
            {
                string userInput = textBox.Text.Trim();
                if (!regexResolution.IsMatch(userInput))
                {
                    MessageBox.Show("Incorrect resolution format.");
                    textBox.Focus();
                    return true;
                }
            }

            textBox = TextBoxSdlWindowresolution;
            if (textBox.Enabled)
            {
                string userInput = textBox.Text.Trim();
                if (!regexResolution.IsMatch(userInput))
                {
                    MessageBox.Show("Incorrect resolution format.");
                    textBox.Focus();
                    return true;
                }
            }

            textBox = TextBoxSdlWindowPosition;
            if (textBox.Enabled)
            {
                string userInput = textBox.Text.Trim();
                if (!regexPosition.IsMatch(userInput))
                {
                    MessageBox.Show("Incorrect position format.");
                    textBox.Focus();
                    return true;
                }
            }

            textBox = TextBoxSdlHostRate;
            if (textBox.Enabled)
            {
                string userInput = textBox.Text.Trim();
                if (!regexNumberGreaterEqual23.IsMatch(userInput))
                {
                    MessageBox.Show("The value must be greater than 23.");
                    textBox.Focus();
                    return true;
                }
            }

            textBox = TextBoxSdlVsyncSkip;
            if (textBox.Enabled)
            {
                string userInput = textBox.Text.Trim();
                if (!regexNumberGreaterEqual0.IsMatch(userInput))
                {
                    MessageBox.Show("The value must be greater than or equal to 0. No decimals allowed.");
                    textBox.Focus();
                    return true;
                }
            }

            // [dosbox]
            textBox = TextBoxDosboxDosRate;

            return false;
        }
        */
    }
}
